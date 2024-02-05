using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Helper;
using SchoolManagement.Application.IdentityModels;
using SchoolManagement.Application.Interface.Service;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Data;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly JWT _jwt;

        public IdentityService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            ITeacherService teacherService,
            IStudentService studentService,
             IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _teacherService = teacherService;
            _studentService = studentService;
            _jwt = jwt.Value;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (model.UserName == null)
                return new AuthModel { Message = "UserName is Required" };
            if (model.FirstName == null)
                return new AuthModel { Message = "FirstName is Required" };
            if (model.LastName == null)
                return new AuthModel { Message = "LastName is Required" };
            if (model.Email == null)
                return new AuthModel { Message = "Email is Required" };
            if (model.Password == null)
                return new AuthModel { Message = "Password is Required" };
            if (model.ConfirmPassword != model.Password)
                return new AuthModel { Message = "!Not Match" };
            var userwithsameUsername = await _userManager.FindByNameAsync(model.UserName);

            if (userwithsameUsername != null)
                return new AuthModel { Message = "UserName is already Taken", UserName = model.UserName };

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FisrtName = model.FirstName,
                LastName = model.LastName,
                Role = model.Role
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email!);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.Role.Equals(Roles.Teacher))
                    {
                        var teacherId = await _teacherService.CreateAsync(new Teacher
                        {
                            Email = model.Email,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            UserName = model.UserName,
                            Age = model.Age,
                            Address = model.Address,
                            ApplicationUserId = Guid.Parse(user.Id)
                        });
                    }
                    if (model.Role.Equals(Roles.Student))
                    {
                        var studentId = await _studentService.CreateAsync(new Student
                        {
                            Email = model.Email,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            UserName = model.UserName,
                            Age = model.Age,
                            Address = model.Address,
                            ApplicationUserId = Guid.Parse(user.Id),
                            ClassId=model.ClassId
                        });
                    }
                    var jwtSecurityToken = await CreateJwtToken(user);
                    return new AuthModel
                    {
                        Email = user.Email,
                        IsAuthenticated = true,
                        ExpiresOn = jwtSecurityToken.ValidTo,
                        Roles = new List<string> { $"{user.Role}" },
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        UserName = user.UserName,
                    };
                }
                else
                {
                    throw new CustomException($"{result.Errors}");
                }
            }
            else
            {
                throw new CustomException($"Email {model.Email} is already registered.");
            }
        }
        public async Task<AuthModel> LoginAsync(LoginModel model)
        {
            var authmodel = new AuthModel();
            if (model == null)
                return new AuthModel { Message = "Email and Password are Required" };

            var user = await _userManager.FindByEmailAsync(model.Email!);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.PassWord!))
            {
                return new AuthModel { Message = "Email Or Password Incorrect" };
            }
            var jwtSecurityToken = await CreateJwtToken(user);

            string roleList = user.Role.ToString();

            authmodel.IsAuthenticated = true;
            authmodel.Email = user.Email;
            authmodel.UserName = user.UserName;
            authmodel.ExpiresOn = jwtSecurityToken.ValidTo;
            authmodel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authmodel.Roles = roleList.Split(',').ToList();
            return authmodel;
        }
        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid userId Or Role";
            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";
            var result = await _userManager.AddToRoleAsync(user, model.Role);
            return result.Succeeded ? string.Empty : "Something went Wrong";
        }

        //public Task<AuthModel> LogoutAsync()
        //{
        //    throw new NotImplementedException();
        //}s

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<string> CreateRoleAsync(string rolename)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole { Name = rolename });

            return result.Succeeded ? string.Empty : "Something went Wrong";
        }
    }
}

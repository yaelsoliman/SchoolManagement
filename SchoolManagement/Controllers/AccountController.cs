using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.IdentityModels;
using SchoolManagement.Application.Interface.Service;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Extensions;
using SchoolManagement.Helper;
using Shared.Common;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(IIdentityService identityService,RoleManager<IdentityRole> roleManager)
        {
            _identityService = identityService;
            _roleManager = roleManager;
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _identityService.RegisterAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _identityService.LoginAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }
        
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddToRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _identityService.AddRoleAsync(model);
            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);
            return Ok(model);

        }

        [HttpGet("updateConcurrencyStamp/{roleName}")]
        public async Task<IActionResult> UpdateConcurrencyStamp(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if(role is not null)
            {
                role.ConcurrencyStamp=Guid.NewGuid().ToString();
                var result=await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                    return Ok("Updated successfully");
                else
                {
                    return BadRequest(result);
                }
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost("Create Role")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _identityService.CreateRoleAsync(roleName);
            return Ok(result);
        }
    }
}

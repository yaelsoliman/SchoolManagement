using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.IdentityModels;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Seed
{
    public class SeedUsers
    {
        public static async Task SeedSuberAdminUser(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                Id=Guid.NewGuid().ToString(),
                UserName ="SuberAdmin",
                FisrtName="Manager",
                LastName="Manager",
                Email="SuberAdmin@gmail.com",
                EmailConfirmed=true,
                PhoneNumber="0999999999"
            };
            var user=await userManager.FindByEmailAsync(defaultUser.Email);
            if(user is null)
            {
                await userManager.CreateAsync(defaultUser,"Passw0rd@123");
                await userManager.AddToRolesAsync(defaultUser, new List<string>(){Roles.SuberAdmin.ToString(),Roles.Teacher.ToString(),Roles.Student.ToString()});
            }

        }
    }
}

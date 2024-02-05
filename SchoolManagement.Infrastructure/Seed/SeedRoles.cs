using Microsoft.AspNetCore.Identity;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Seed
{
    public class SeedRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.SuberAdmin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Teacher.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Student.ToString()));
            }
        }
    }
    
}

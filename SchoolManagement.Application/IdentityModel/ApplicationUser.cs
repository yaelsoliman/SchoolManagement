using Microsoft.AspNetCore.Identity;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.IdentityModels
{
    public class ApplicationUser:IdentityUser
    {
        [MaxLength(50)]
        public string? FisrtName { get; set; }
        [MaxLength(50)]

        public string? LastName { get; set; }
        public Roles Role { get; set; }
    }
}

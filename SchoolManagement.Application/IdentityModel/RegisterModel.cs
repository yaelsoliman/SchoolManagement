using Shared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.IdentityModels
{
    public class RegisterModel
    {
        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set;}

        [StringLength(100)]
        public string? UserName { get; set; }

        [StringLength(128)]
        public string? Email { get; set; }

        [StringLength(256)]
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

        [StringLength(256)]
        public string? Address { get; set; }

        public int Age { get; set; }
        public Guid ClassId { get; set; }
        public string? ClassName { get; set; }
        public Roles Role { get; set; }

    }
}

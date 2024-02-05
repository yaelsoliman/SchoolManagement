using Ganss.XSS;
using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Student : BaseEntity
    {
        private string address;

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$",ErrorMessage ="First Name must contain only letters and Capital letter at the first")]
        [StringLength(15,MinimumLength =3)]
        public string? FirstName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Last Name must contain only letters and  Capital letter at the first")]
        [StringLength(15, MinimumLength = 3)]
        public string? LastName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "UserName must contain only letters and  Capital letter at the first")]
        [StringLength(30, MinimumLength = 3)]
        public string? UserName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Address { get; set; }
        
        public int Age { get; set; }
        public Guid ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public virtual Class? Class { get; set; }
        public Guid ApplicationUserId { get; set; }


        //For Secure Coding

        //public string Addrees
        //{
        //    get => address;
        //    set => address = Regex.Replace(value, @"[\!\@\#\$\%\^\&\*\<\>\|{\}\?\;\[\]\~]+", string.Empty);
        //}

        //Another Secure
        //public string Addrees
        //{
        //    get => address;
        //    set => address =new  HtmlSanitizer().Sanitize(value);
        //}
    }
}

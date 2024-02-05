using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Dto
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int Age { get; set; }
        public string? DeleteName { get; set; }
        public ClassDto Class { get; set; }
        public Guid ClassId { get; set; }
    }
}

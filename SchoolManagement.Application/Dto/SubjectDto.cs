using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Dto
{
    public class SubjectDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? IsActive { get; set; }
        public string? DeletedName { get; set; }
    }
}

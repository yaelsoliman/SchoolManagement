using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Dto
{
    public class TSCDto
    {
        public Guid tscId { get; set; }
        public Guid TeacherId { get; set; }
        //public Teacher? Teacher { get; set; }
        public Guid ClassId { get; set; }
        //public Class? Class { get; set; }
        public Guid SubjectId { get; set; }
        //public Subject? Subject { get; set; }
        public bool IsActive { get; set; }
    }
    public class TSCDtoMain
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public TeacherDto? Teacher { get; set; }
        public Guid ClassId { get; set; }
        public ClassDto? Class { get; set; }
        public Guid SubjectId { get; set; }
        public SubjectDto? Subject { get; set; }
        public bool IsActive { get; set; }
    }
}

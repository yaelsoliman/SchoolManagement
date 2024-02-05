using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Dto
{
    public class MarkDto
    {
        public Guid Id { get; set; }
        public int Mark { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public DateTime Date { get; set; }

    }
    public class MarkMainDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
    }
    public class MarkListDto:MarkMainDto
    {
        public int Mark { get; set; }
        public Guid StudentId { get; set; }
        public StudentDto Student { get; set; }
        public Guid SubjectId { get; set; }
        public SubjectDto Subject { get; set; }



    }
    public class MarkDtoMin
    {
        public int Mark { get; set; }
        public DateTime Date { get; set; }
        public Guid StudentId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Dto
{
    public class NoteDto
    {
        public string? NoteDescription { get; set; }
        public Guid StudentId { get; set; }
        public DateTime Date { get; set; }
    }
}

using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Notes : BaseEntity
    {
        public string? NoteDescription { get; set; }
        public Guid StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public virtual Student? Student { get; set; }

        //public string? StudentName { get; set; }
        public DateTime Date { get; set; }
    }
}

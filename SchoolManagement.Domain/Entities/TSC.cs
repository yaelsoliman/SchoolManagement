using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class TSC : BaseEntity
    {
        public Guid TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]

        public Teacher? Teacher { get; set; }
        public Guid ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]

        public Class? Class { get; set; }
        public Guid SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]

        public Subject? Subject { get; set; }
    }
}

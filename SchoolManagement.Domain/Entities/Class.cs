using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Class:BaseEntity
    {
        public string? Name { get; set; }
       
        public virtual ICollection<Student>? Students { get; set; }
    }
}

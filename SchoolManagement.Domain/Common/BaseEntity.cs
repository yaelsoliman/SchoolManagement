using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Common
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
        public string? DeleteName { get; set; }
        public bool IsActive { get; set; }
    }

    public class BaseEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string? DeleteName { get; set; }

        public bool IsActive { get; set; }
    }
}

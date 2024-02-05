using System.Text.Json.Serialization;

namespace SchoolManagement.Application.Dto
{
    public class ClassDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public bool IsActive { get; set; }
        public string? DeleteName { get; set; }
        public StudentDto? StudentDto { get; set; }
    }
}

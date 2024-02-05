using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using rm.Extensions;
using SchoolManagement.Application.Dto;
using SchoolManagement.Application.Interface.Service;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Extensions;
using SchoolManagement.Infrastructure.Data;
using SchoolManagement.Infrastructure.Service;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ApplicationDbContext _dbContext;

        public StudentController(IStudentService studentService, ApplicationDbContext dbContext)
        {
            _studentService = studentService;
            _dbContext = dbContext;
        }

        [HttpGet("studentId")]
        public async Task<IActionResult> GetStudentAsync(Guid studentId)
        {
            if (studentId == Guid.Empty)
                return NotFound();
            var student=await _studentService.GetAsync(studentId);
            if (student is null)
                return NotFound();
            //HttpContext.Session.SetString("KeyName", JsonConvert.SerializeObject(student));
            return Ok(student);
        }


        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var students = await _studentService.GetListAsync(x=>x.Class!);
            if (students.IsEmpty())
                return NotFound();
            return Ok(students.AsRespons<IEnumerable<StudentDto>>());
        }

        [HttpGet("Active")]
        public async Task<IActionResult> GetAllStudentsActive()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Students = await _studentService.GetListAsync();
            var studentsNull = Students.Where(x => x.DeleteName == null || x.DeleteName == "null").ToList();
            if (studentsNull.IsEmpty())
                return NotFound();
            return Ok(studentsNull.AsRespons<IEnumerable<StudentDto>>());

        }
        [HttpGet("deleted")]
        public async Task<IActionResult> GetAllStudentsdeleted()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Students = await _studentService.GetListAsync();
            var studentsNull = Students.Where(x => x.DeleteName == "deleted").ToList();
            if(studentsNull.IsEmpty())
                return NotFound();
            return Ok(studentsNull.AsRespons<IEnumerable<StudentDto>>());

        }

        [HttpGet("classId")]
        public async Task<IActionResult> GetAllStudentsByClass(Guid classId)
        {
            if (classId == Guid.Empty)
                return NotFound();
            var students = await _studentService.GetListAsync(x => x.ClassId == classId);
            if (students.IsEmpty())
                return NotFound();
            return Ok(students.AsRespons<IEnumerable<StudentDto>>());
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteStudentAsync(Guid studentId)
        {
            if (studentId == Guid.Empty)
                return NotFound();

            var student = await _studentService.GetAsync(studentId);
            if (student is null)
                return NotFound();
            await _studentService.DeleteAsync(student.Id);
            return Ok(student.AsRespons<StudentDto>());
        }
        [HttpPut("studentId")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute]Guid studentId, [FromBody] StudentDto studentDto)
        {
            if (studentId == Guid.Empty)
                return NotFound();
            var student = await _studentService.GetAsync(studentId);
            if (student is null)
                return NotFound();
            student.FirstName = studentDto.FirstName;
            student.LastName = studentDto.LastName;
            student.Email = studentDto.Email;
            student.UserName = studentDto.UserName;
            student.Address = studentDto.Address;
            student.Age = studentDto.Age;
            student.DeleteName = studentDto.DeleteName;
            await _studentService.UpdateAsync(student);
            return Ok(student.AsRespons<StudentDto>());
            //var KeyName = HttpContext.Session.GetString("KeyName");
            //var StudentSerialize = JsonConvert.DeserializeObject<Student>(KeyName);

            //StudentSerialize.FirstName = studentDto.FirstName;
            //StudentSerialize.LastName = studentDto.LastName;
            //StudentSerialize.Email = studentDto.Email;
            //StudentSerialize.UserName = studentDto.UserName;
            //StudentSerialize.Address = studentDto.Address;
            //StudentSerialize.Age = studentDto.Age;
        }
        [HttpPost("studentId")]
        public async Task<IActionResult> ToggleActiveAsync(Guid studentId)
        {
            var student = await _studentService.ToggleActive(studentId);
            return Ok(student.AsRespons<StudentDto>());
        }
    }
}

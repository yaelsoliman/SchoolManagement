using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rm.Extensions;
using SchoolManagement.Application.Dto;
using SchoolManagement.Application.Interface.Service;
using SchoolManagement.Extensions;
using SchoolManagement.Infrastructure.Service;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeacher()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Teachers = await _teacherService.GetListAsync();
            if (Teachers.IsEmpty())
                return NotFound();
            return Ok(Teachers.AsRespons<IEnumerable<TeacherDto>>());
        }
        [HttpGet("teacherId")]
        public async Task<IActionResult> GetTeacherAsync(Guid teacherId)
        {
            if (teacherId == Guid.Empty)
                return NotFound();
            var teacher = await _teacherService.GetAsync(teacherId);
            if (teacher is null)
                return NotFound();
            return Ok(teacher);
        }
        [HttpGet("Active")]
        public async Task<IActionResult> GetAllTeachersActive()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Teachers = await _teacherService.GetListAsync();
            var teachersNull = Teachers.Where(x => x.DeleteName == null || x.DeleteName == "null").ToList();
            if (teachersNull.Count == 0)
                return NotFound();
            return Ok(teachersNull.AsRespons<IEnumerable<TeacherDto>>());

        }
        [HttpGet("deleted")]
        public async Task<IActionResult> GetAllTeachersdeleted()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Teachers = await _teacherService.GetListAsync();
            var teachersNull = Teachers.Where(x => x.DeleteName == "deleted").ToList();
            if (teachersNull.Count == 0)
                return NotFound();
            return Ok(teachersNull.AsRespons<IEnumerable<TeacherDto>>());

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTeacherAsync(Guid teacherId)
        {
            if (teacherId == Guid.Empty)
                return NotFound();

            var teacher = await _teacherService.GetAsync(teacherId);
            if (teacher is null)
                return NotFound();
            await _teacherService.DeleteAsync(teacher.Id);
            return Ok(teacher.AsRespons<TeacherDto>());
        }
        [HttpPut]
        public async Task<IActionResult> UpdateteacherAsync(Guid teacherId, [FromBody] TeacherDto teacherDto)
        {
            if (teacherId == Guid.Empty)
                return NotFound();
            if (teacherDto == null)
                return BadRequest(ModelState);
            var teacher = await _teacherService.GetAsync(teacherId);
            if (teacher is null)
                return NotFound();
            teacher.Email = teacherDto.Email;
            teacher.UserName = teacherDto.UserName;
            teacher.FirstName = teacherDto.FirstName;
            teacher.LastName = teacherDto.LastName;
            teacher.Address = teacherDto.Address;
            teacher.Age = teacherDto.Age;
            teacher.DeleteName = teacherDto.DeletedName;
            await _teacherService.UpdateAsync(teacher);
            return Ok(teacher.AsRespons<TeacherDto>());
        }
        [HttpPost("teacherId")]
        public async Task<IActionResult> ToggleActiveAsync(Guid teacherId)
        {
            var teacher = await _teacherService.ToggleActive(teacherId);
            return Ok(teacher.AsRespons<TeacherDto>());
        }
    }
}

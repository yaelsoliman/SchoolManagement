using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Dto;
using SchoolManagement.Application.Interface.Service;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Extensions;
using SchoolManagement.Infrastructure.Data;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly IMarksService _marksService;
        private readonly ApplicationDbContext _dbContext;

        public MarksController(IMarksService marksService, ApplicationDbContext dbContext)
        {
            _marksService = marksService;
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> CreateMark([FromBody] MarkDto markDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (markDto == null)
                return BadRequest(ModelState);
            var mark = new Marks()
            {
                StudentId = markDto.StudentId,
                SubjectId = markDto.SubjectId,
                Mark = markDto.Mark,
                Date = markDto.Date,
            };
            await _marksService.CreateAsync(mark);
            return Ok(mark);
        }
        [HttpGet("studentId")]
        public async Task<IActionResult> GetAllMarksForStudent(Guid studentId)
        {
            if (studentId == Guid.Empty)
                return NotFound();
            var marks = await _marksService.GetListAsync(u => u.StudentId == studentId, c => c.Subject!, x => x.Student!);
            if (marks == null)
                return NotFound();
            return Ok(marks.AsRespons<IEnumerable<MarkListDto>>());
        }

        [HttpGet("MaxMark/subjectId")]
        public async Task<IActionResult> GetMaxMarkOrder(Guid subjectId)
        {
            if (subjectId == Guid.Empty)
                return NotFound();
            var marks = await _marksService.GetListAsync(x => x.SubjectId == subjectId, c => c.Subject!, x => x.Student!);
            var maxMark = marks.OrderByDescending(x => x.Mark);
            return Ok(maxMark.AsRespons<IEnumerable<MarkListDto>>());
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMark(Guid markId, [FromBody] MarkDtoMin markDtomin)
        {
            if (markId == Guid.Empty)
                return NotFound();
            if (markDtomin is null)
                return BadRequest();
            var mark = await _marksService.GetAsync(markId, x => x.Student!);
            if (mark == null) return NotFound();
            mark.Mark = markDtomin.Mark;
            mark.Date = markDtomin.Date;
            await _marksService.UpdateAsync(mark);
            return Ok(mark.AsRespons<MarkDtoMin>());

        }


    }
}

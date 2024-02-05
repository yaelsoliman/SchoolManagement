using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rm.Extensions;
using SchoolManagement.Application.Dto;
using SchoolManagement.Application.Interface.Service;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] NoteDto noteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (noteDto == null)
                return BadRequest(ModelState);
            var Note = new Notes()
            {
                NoteDescription = noteDto.NoteDescription,
                StudentId = noteDto.StudentId,
                Date = DateTime.Now,
                IsActive = true
            };
            await _noteService.CreateAsync(Note);
            return Ok(Note);
        }
        [HttpGet]
        public async Task<IActionResult> GetStudentNote(Guid studentId)
        {
            if (studentId == Guid.Empty)
                return NotFound();
            var Note = await _noteService.GetListAsync(x => x.StudentId == studentId);
            if (Note.IsEmpty())
                return NotFound();
            return Ok(Note);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateNote(Guid noteId, [FromBody] NoteDto noteDto)
        {
            if (noteId == Guid.Empty)
                return NotFound();
            if (noteDto == null)
                return BadRequest(ModelState);
            var note = await _noteService.GetAsync(noteId);
            if (note is null)
                return NotFound();
            note.NoteDescription = noteDto.NoteDescription;
            note.StudentId = noteDto.StudentId;
            await _noteService.UpdateAsync(note);
            return Ok(note);
        }
    }
}

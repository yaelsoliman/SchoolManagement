using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rm.Extensions;
using SchoolManagement.Application.Dto;
using SchoolManagement.Application.Interface.Service;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Extensions;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectsService _subjectsService;

        public SubjectsController(ISubjectsService subjectsService)
        {
           _subjectsService = subjectsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var subjects=await _subjectsService.GetListAsync();
            if(subjects.IsEmpty())
                return NotFound();
            return Ok(subjects.AsRespons<IEnumerable<SubjectDto>>());

        }
    }
}

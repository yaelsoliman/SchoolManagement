using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Dto;
using SchoolManagement.Application.Interface.Service;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Extensions;
using SchoolManagement.Helper;
using SchoolManagement.Infrastructure.Data;
using Shared.Common;
using System.Linq;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly ApplicationDbContext _dbContext;

        public ClassController(IClassService classService,ApplicationDbContext dbContext)
        {
            _classService = classService;
            _dbContext = dbContext;
        }

        
        [HttpGet("GetAllClasses")]
       
        public async Task<IActionResult> GetAllClasses()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var classes = await _classService.GetListAsync(s=>s.Students!);
               
            if (classes is null)
                return NotFound();
            return Ok(classes/*.AsRespons<IEnumerable<ClassDto>>()*/);
            
        }
        [HttpGet("classId")]
        public async Task<IActionResult> GetClass(Guid classId)
        {
            if (classId == Guid.Empty)
                return NotFound();
            var Class = await _classService.GetAsync(classId);
            if (Class is null)
                return NotFound();
            return Ok(Class.AsRespons<ClassDto>());

        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateClass([FromBody] ClassDto classDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Class = new Class()
            {
                Name = classDto.Name,
                IsActive = classDto.IsActive,
            };
            await _classService.CreateAsync(Class);
            return Ok(Class.AsRespons<ClassDto>());
        }
        [HttpPut("classId")]
        public async Task<IActionResult> UpdateClass(Guid classId, [FromBody] ClassDto classDto)
        {
            if (classId == Guid.Empty)
                return NotFound();
            var Class = await _classService.GetAsync(classId);
            if (Class.Name.IsEmpty())
            {
                ModelState.AddModelError("", "The Field Name is Empty");
                return StatusCode(422, ModelState);
            }
            Class.Name = classDto.Name;
            Class.IsActive = classDto.IsActive;
            await _classService.UpdateAsync(Class);
            return Ok(Class.AsRespons<ClassDto>());
        }
        [HttpPost("classId")]
        public async Task<IActionResult> ToggleActiveAsync(Guid classId)
        {
            var Class=await _classService.ToggleActive(classId);
            return Ok(Class);
        }
    }
}

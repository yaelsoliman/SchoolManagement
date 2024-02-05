using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Dto;
using SchoolManagement.Application.Interface.Service;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Extensions;
using SchoolManagement.Infrastructure.Service;
using System.Runtime.CompilerServices;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TSCController : ControllerBase
    {
        private readonly ITSCService _tSCService;
       

        public TSCController(ITSCService tSCService)
        {
            _tSCService = tSCService;
            
        }
        [HttpGet("tscId")]
        public async Task<IActionResult> GetTSCAsync(Guid tscId)
        {
            if (tscId == Guid.Empty)
                return NotFound();
            var tsc = await _tSCService.GetAsync(tscId, x => x.Teacher!, c => c.Subject!, y => y.Class!);
            if (tsc is null)
                return NotFound();
            return Ok(tsc.AsRespons<TSCDtoMain>());

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTSCAsync()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var tsc = await _tSCService.GetListAsync(x => x.Teacher!, c => c.Subject!, y => y.Class!);
            if (tsc is null)
                return NotFound();
            return Ok(tsc.AsRespons<IEnumerable<TSCDtoMain>>());

        }


        [HttpPost]
        public async Task<IActionResult> CreateTSCAsync([FromBody] TSCDto tSCDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (tSCDto is null)
                return BadRequest(ModelState);
            var tsc = new TSC()
            {
                TeacherId = tSCDto.TeacherId,
                SubjectId = tSCDto.SubjectId,
                ClassId = tSCDto.ClassId,
                IsActive = tSCDto.IsActive,
            };
            await _tSCService.CreateAsync(tsc);
            return Ok(tsc);
        }
    }
}

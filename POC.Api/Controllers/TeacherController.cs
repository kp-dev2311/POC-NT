using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POCNT.Application.DTOs;
using POCNT.Application.Services;
using POCNT.Infrastructure.Data;

namespace POCNT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherService _teacherService;

        public TeacherController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _teacherService.GetTeachersAsync();
            if (teachers == null)
            {
                return NotFound();
            }
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound($"The Teacher with id: {id} is not found");
            }
            try
            {
                var teacher = await _teacherService.GetByIdAsync(id);
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] TeacherDto teacherDto)
        {
            if (teacherDto == null)
            {
                return BadRequest(new { message = "Teacher data is required." });
            }

            try
            {
                await _teacherService.CreateTeacherAsync(teacherDto);
                return CreatedAtAction(nameof(GetTeacher), new { teacher = teacherDto.Id }, teacherDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] TeacherDto teacherDto)
        {
            if(teacherDto == null || id == null)
            {
                return NotFound();
            }
            try
            {
                await _teacherService.UpdateTeacherAsync(id, teacherDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }  

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            try { 
            await _teacherService.DeleteTeacherAsync(id);
            return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured while processing your request");
            };
        }
    }
}

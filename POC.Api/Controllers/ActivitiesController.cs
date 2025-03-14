using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POCNT.Application.DTOs;
using POCNT.Application.Services;

namespace POCNT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly UserActivitiesService _userActivitiesServiceService;

        public ActivitiesController(UserActivitiesService userActivitiesServiceService)
        {
            _userActivitiesServiceService = userActivitiesServiceService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetUserActivities()
        {
            try
            {
                var students = await _userActivitiesServiceService.GetUserActivitiesAsync();
                if (students == null)
                {
                    return NotFound();
                }
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpGet("most-active")]
        public async Task<IActionResult> MostActiveUsers()
        {
            try
            {
                var students = await _userActivitiesServiceService.GetUserActivitiesAsync();
                if (students == null)
                {
                    return NotFound();
                }
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("average-session")]
        public async Task<IActionResult> UsersAverageSession()
        {
            try
            {
                var students = await _userActivitiesServiceService.GetUserActivitiesAsync();
                if (students == null)
                {
                    return NotFound();
                }
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("active-users")]
        public async Task<IActionResult> ActiveUsers()
        {
            try
            {
                var students = await _userActivitiesServiceService.GetUserActivitiesAsync();
                if (students == null)
                {
                    return NotFound();
                }
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPost("SeedDataToSaveForTesting")]
        public async Task<IActionResult> SaveUserActivites([FromBody] ActivityReqDto studentDto)
        {
            if (studentDto == null)
            {
                return NotFound();
            }
            try
            {
                await _userActivitiesServiceService.CreateStudentAsync(studentDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        

    }
}

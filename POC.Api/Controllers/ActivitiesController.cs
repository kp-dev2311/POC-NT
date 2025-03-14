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
                var userObj = await _userActivitiesServiceService.GetUserActivitiesAsync();
                if (userObj == null)
                {
                    return NotFound();
                }
                return Ok(userObj);
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
                var userActivities = await _userActivitiesServiceService.GetMostActiveUserAsync();
                if (userActivities == null)
                {
                    return NotFound();
                }
                return Ok(userActivities);
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
                var userActObj = await _userActivitiesServiceService.GetAvgActivitesUserAsync();
                if (userActObj == null)
                {
                    return NotFound();
                }
                return Ok(userActObj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("active-users")]
        public async Task<IActionResult> ActiveUsers(int days = 1)
        {
            try
            {
                DateTime toDate = DateTime.UtcNow;
                DateTime fromDate = DateTime.UtcNow.AddDays(-1* days);
                var usersObj = await _userActivitiesServiceService.GetDurationBasedActiveUsersAsync(fromDate,toDate);
                if (usersObj == null)
                {
                    return NotFound();
                }
                return Ok(usersObj);
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
                await _userActivitiesServiceService.CreateUserActivitiesAsync(studentDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POCNT.Application.DTOs;
using POCNT.Application.Services;

namespace POCNT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _adminService;

        public UsersController(UsersService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdmins()
        {
            try { 
            var admins = await _adminService.GetAdminsAsync();
            if(admins == null)
            {
                return NotFound();
            };
            return Ok(admins);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdmin(int id)
        {
            try { 
                var admin = await _adminService.GetByIdAsync(id);
                if (admin == null)
                {
                    return NotFound();
                }
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured while processing the request");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromBody] UsersDto adminDto) 
        {
            if(adminDto == null)
            {
                return BadRequest("Data is required to process the request..");
            }
            try { 
                await _adminService.CreateAdminAsync(adminDto);
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] UsersDto adminDto)
        {
            if (adminDto == null || id == null)
            {
                return NotFound();
            }
            await _adminService.UpdateAdminAsync(id, adminDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            try { 
            await _adminService.DeleteAdminAsync(id);
            return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}

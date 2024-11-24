using TodoApi.Models.Staff;
using TodoApi.DTOs;
using TodoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TodoApi.Controllers
{
    [Route("api/staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("create")]
        public async Task<IActionResult> RegisterStaff([FromBody] CreateStaffDTO dto)
        {
            try
            {
                var staff = await _staffService.CreateStaff(dto);
                return Ok(staff);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "BackOfficeUserPolicy")]        
        [HttpGet]
        public async Task<IActionResult> GetStaff()
        {
            try
            {
                var allstaffMembers = await _staffService.GetStaff();
                return Ok(allstaffMembers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchStaff(
            [FromQuery] string? fullName = null,
            [FromQuery] string? specialization = null,
            [FromQuery] string? email = null,
            [FromQuery] string? status = null,
            [FromQuery] string? phone = null)
        {
            try
            {
                var results = await _staffService.SearchStaff(fullName, specialization, email, status, phone);
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpPut("update/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateStaff(Guid id, [FromBody] UpdateStaffDTO dto)
        {
            try
            {
                var updatedStaffDto = await _staffService.UpdateStaff(id, dto);
                return Ok(updatedStaffDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> InactivateStaff(Guid id, [FromBody] UpdateStaffDTO dto)
        {   try
            {
                var updatedStaffStatusDto = await _staffService.InactivateStaff(id, dto);
                return Ok(updatedStaffStatusDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
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

        [Authorize(Policy = "AdminPolicy")]
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
        [HttpGet("search/specialization/{specialization}")]
        public async Task<ActionResult<List<object>>> SearchBySpecialization(string specialization)
        {
            try
            {
                var staffspecialization = await _staffService.GetStaffBySpecialization(specialization);
                return Ok(staffspecialization);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("search/name/{name}")]
        public async Task<ActionResult<List<StaffDTO>>> SearchByName(string name)
        {
            try
            {
                var staffname = await _staffService.GetStaffByName(name);
                return Ok(staffname);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("search/email/{email}")]
        public async Task<ActionResult<List<StaffDTO>>> SearchByEmail(string email)
        {
            try
            {
                var staffemail = await _staffService.GetStaffByEmail(email);
                return Ok(staffemail);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("search/status/{status}")]
        public async Task<ActionResult<List<StaffDTO>>> SearchByStatus(StaffStatus status)
        {
            try
            {
                var staffStatusList = await _staffService.GetStaffByStatus(status);
                return Ok(staffStatusList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
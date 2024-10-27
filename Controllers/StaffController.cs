using TodoApi.Models.Staff;
using TodoApi.DTOs;
using TodoApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
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

        [HttpPost("GoThroughAuthorizeAsync")]
        public async Task<IActionResult> GoThroughAuthorizeAsync([FromBody] string url) {
            
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            var access_token = HttpContext.Session.GetString("AccessToken");

            using (var client = new HttpClient())
            {
                // Add the Authorization header with Bearer token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                // Make the authorized request

                var response = await client.GetAsync($"http://localhost:5012/api/staff/{url}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Content(data);
                }
                else
                {
                    return Unauthorized();
                }
            }
        }

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

        [Authorize]
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

        [HttpGet("search/specialization")]
        public async Task<ActionResult<List<object>>> SearchBySpecialization([FromQuery] string specialization)
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

        [HttpGet("search/name")]
        public async Task<ActionResult<List<StaffDTO>>> SearchByName([FromQuery] string name)
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

        [HttpGet("search/email")]
        public async Task<ActionResult<List<StaffDTO>>> SearchByEmail([FromQuery] string email)
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
        [HttpGet("search/status")]
        public async Task<ActionResult<List<StaffDTO>>> SearchByStatus([FromQuery] StaffStatus status)
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
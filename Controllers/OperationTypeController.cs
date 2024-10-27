
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs.OperationType;
using TodoApi.Services.OperationType;

namespace TodoApi.Controllers;

[Route("api/OperationType")]
[ApiController]
public class OperationTypeController : ControllerBase
{
    private readonly IOperationTypeService _operationTypeService;

    public OperationTypeController(IOperationTypeService operationTypeService)
    {
        _operationTypeService = operationTypeService;
    }


        [HttpPost("GoThroughAuthorizeAsync/{url}")]
        public async Task<IActionResult> GoThroughAuthorizeAsync(string url) {
            
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            var access_token = HttpContext.Session.GetString("AccessToken");

            using (var client = new HttpClient())
            {
                // Add the Authorization header with Bearer token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                // Make the authorized request

                var response = await client.GetAsync($"http://localhost:5012/api/OperationType/{url}");

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

    [HttpPost]
    public async Task<IActionResult> CreateOperationType([FromBody] CreateOperationTypeDTO createOperationTypeDto)
    {
        if (createOperationTypeDto == null)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            OperationTypeDTO result = await _operationTypeService.CreateOperationType(createOperationTypeDto);
            return CreatedAtAction(nameof(CreateOperationType), new { id = result.OperationTypeId }, result);
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateOperationType(Guid id, [FromBody] UpdateOperationTypeDTO dto)
    {
        try
        {
            var updatedOperationTypeDto = await _operationTypeService.UpdateOperationTypeAsync(id, dto);                
            return Ok(updatedOperationTypeDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("delete/{id}")]
    public async Task<IActionResult> DeleteOperation([FromQuery] Guid operationId)
    {
        try
        {
            var updatedOperationTypeDto = await _operationTypeService.DeleteOperationType(operationId);                
            return Ok(updatedOperationTypeDto);
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
                var operationTypes = await _operationTypeService.GetOperationTypes();
                return Ok(operationTypes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("search/specialization/{specialization}")]
        public async Task<ActionResult<List<OperationTypeDTO>>> SearchBySpecialization(string specialization)
        {
            try
            {
                var operationTypes = await _operationTypeService.GetOperationTypesBySpecialization(specialization);
                return Ok(operationTypes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Authorize]
        [HttpGet("search/name/{name}")]
        public async Task<ActionResult<List<OperationTypeDTO>>> SearchByName(string name)
        {
            try
            {
                var operationTypes = await _operationTypeService.GetOperationTypesByName(name);
                return Ok(operationTypes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("search/status/{status}")]
        public async Task<ActionResult<List<OperationTypeDTO>>> SearchByStatus(bool status)
        {
            try
            {
                var operationTypes = await _operationTypeService.GetOperationTypesByStatus(status);
                return Ok(operationTypes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
}
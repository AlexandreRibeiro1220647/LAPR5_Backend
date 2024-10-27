
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

namespace TodoApi.Controllers;

[Route("api/operations")]
[ApiController]
public class OperationRequestController : ControllerBase {
     
     private readonly IOperationRequestService operationRequestService;

     public OperationRequestController(IOperationRequestService operationRequestService){
        this.operationRequestService = operationRequestService;
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

                var response = await client.GetAsync($"http://localhost:5012/api/operations/{url}");

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
    public async Task<IActionResult> CreateOperation([FromBody] CreateOperationRequestDTO dto)
    {
        try
        {
            var operation = await operationRequestService.CreateOperationRequest(dto);
            return Ok(operation);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOperation([FromQuery] Guid operationId)
    {
        try
        {
            await operationRequestService.DeleteOperationRequest(operationId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateOperationRequest(Guid id, [FromBody] UpdateOperationRequestDTO dto)
    {
        try
        {
            var updatedOperationRequestDto = await operationRequestService.UpdateOperationRequestAsync(id, dto);                
            return Ok(updatedOperationRequestDto);
        }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetOperations()
    {
        try
        {
            var operation = await operationRequestService.GetOperations();
            return Ok(operation);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
     [HttpGet("search")]
    public async Task<IActionResult> SearchOperationRequests([FromQuery] string? patientName, [FromQuery] string? patientId, [FromQuery] string? operationType, [FromQuery] string? priority, [FromQuery] string? status)
    {
        try
        {
            List<OperationRequestDTO> results = await operationRequestService.SearchOperations(patientName, patientId, operationType, priority, status);
            return Ok(results);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }

}




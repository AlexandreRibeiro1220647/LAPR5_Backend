
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

    [Authorize(Policy = "DoctorPolicy")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateOperation([FromBody] CreateOperationRequestDTO dto)
    {
        try
        {
            OperationRequestDTO operation = await operationRequestService.CreateOperationRequest(dto);
            return CreatedAtAction(nameof(CreateOperation), new { id = operation.operationId }, operation);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = "DoctorPolicy")]
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

    [Authorize(Policy = "DoctorPolicy")]
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

    [Authorize(Policy = "DoctorPolicy")]    
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

    [Authorize(Policy = "DoctorPolicy")]
    [HttpGet("search")]
    public async Task<IActionResult> SearchOperationRequests([FromQuery] string? patientName, [FromQuery] string? patientId, [FromQuery] string? operationType, [FromQuery] string? priority, [FromQuery] string? deadline)
    {
        try
        {
            List<OperationRequestDTO> results = await operationRequestService.SearchOperations(patientName, patientId, operationType, priority, deadline);
            return Ok(results);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }

}




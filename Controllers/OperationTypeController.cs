
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
}
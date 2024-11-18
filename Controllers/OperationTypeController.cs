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

    //[Authorize(Policy = "AdminPolicy")]
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

    //[Authorize(Policy = "AdminPolicy")]
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

    //[Authorize(Policy = "AdminPolicy")]
    [HttpPut("delete/{id}")]
    public async Task<IActionResult> DeleteOperation(Guid id)
    {
        try
        {
            var updatedOperationTypeDto = await _operationTypeService.DeleteOperationType(id);                
            return Ok(updatedOperationTypeDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    //[Authorize(Policy = "AdminPolicy")]
    [HttpGet]
    public async Task<IActionResult> GetOperationTypes()
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

    //[Authorize(Policy = "AdminPolicy")]
    [HttpGet("search")]
    public async Task<ActionResult<List<OperationTypeDTO>>> SearchOperationTypes([FromQuery] string? name, [FromQuery] string? specialization, [FromQuery] string? estimatedDuration, [FromQuery] string? status)
    {
        try
        {
            var operationTypes = await _operationTypeService.SearchOperationTypes(name, specialization, estimatedDuration, status);
            return Ok(operationTypes);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
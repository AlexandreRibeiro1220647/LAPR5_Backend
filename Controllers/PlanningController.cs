using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs.OperationType;
using TodoApi.Services;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlanningController : ControllerBase
{
    private readonly IPlanningService _planningservice;

    public PlanningController(IPlanningService planningservice)
    {
        _planningservice = planningservice;
    }

    [HttpGet("opTypeDuration")]
    public async Task<IActionResult> GetOperationTypeDurations()
    {
        try
        {
            var operationTypes = await _planningservice.GetOperationTypeDurations();
            return Ok(operationTypes);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("opRequestType")]
    public async Task<IActionResult> GetOperationRequestTypes()
    {
        try
        {
            var operationTypes = await _planningservice.GetOperationRequestTypes();
            return Ok(operationTypes);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
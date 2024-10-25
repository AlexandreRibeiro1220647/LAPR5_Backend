
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services.OperationRequest;

namespace TodoApi.Controllers;

[Route("api/operations")]
[ApiController]
public class OperationRequestController : ControllerBase {
     
     private readonly IOperationRequestService operationRequestService;

     public OperationRequestController(IOperationRequestService operationRequestService){
        this.operationRequestService = operationRequestService;
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
/*
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
*/

}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

namespace TodoApi.Controllers;

[Route("api/surgeryRooms")]
[ApiController]
public class SurgeryRoomController : ControllerBase {
     
    private readonly ISurgeryRoomService surgeryRoomService;

    public SurgeryRoomController(ISurgeryRoomService surgeryRoomService){
        this.surgeryRoomService = surgeryRoomService;
    }


    [Authorize(Policy = "BackOfficeUserPolicy")]    
    [HttpGet]
    public async Task<IActionResult> GetSurgeryOperations()
    {
        try
        {
            var surgeryRoom = await surgeryRoomService.GetSurgeryRooms();
            return Ok(surgeryRoom);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}





using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

[Route("api/roomTypes")]
[ApiController]
public class RoomTypeController : ControllerBase {
     
    private readonly IRoomTypeService roomTypeService;

    public RoomTypeController(IRoomTypeService roomTypeService){
        this.roomTypeService = roomTypeService;
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateRoomType([FromBody] CreateRoomTypeDTO dto)
    {
        try
        {
            RoomTypeDTO roomType = await roomTypeService.CreateRoomType(dto);
            return CreatedAtAction(nameof(CreateRoomType), new { id = roomType.roomTypeId }, roomType);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = "BackOfficeUserPolicy")]
    [HttpGet]
    public async Task<IActionResult> GetRoomTypes()
    {
        try
        {
            var roomType = await roomTypeService.GetRoomTypes();
            return Ok(roomType);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

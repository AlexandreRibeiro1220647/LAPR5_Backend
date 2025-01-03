
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

namespace TodoApi.Controllers;

[Route("api/surgeries")]
[ApiController]
public class AppointmentController : ControllerBase {
     
    private readonly IAppointmentSurgeryService appointmentSurgeryService;

    public AppointmentController(IAppointmentSurgeryService appointmentSurgeryService){
        this.appointmentSurgeryService = appointmentSurgeryService;
    }

    [Authorize(Policy = "DoctorPolicy")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateAppointmentSurgery([FromBody] CreateAppointmentSurgeryDTO dto)
    {
        try
        {
            AppointmentSurgeryDTO surgery = await appointmentSurgeryService.CreateAppointmentSurgery(dto);
            return CreatedAtAction(nameof(CreateAppointmentSurgery), new { id = surgery.appointmentSurgeryId }, surgery);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = "DoctorPolicy")]
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAppointmentSurgery(Guid id, [FromBody] UpdateAppointmentSurgeryDTO dto)
    {
        try
        {
            var updatedAppointmentSurgeryDto = await appointmentSurgeryService.UpdateAppointmentSurgeryAsync(id, dto);                
            return Ok(updatedAppointmentSurgeryDto);
        }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    [Authorize(Policy = "DoctorPolicy")]    
    [HttpGet]
    public async Task<IActionResult> GetSurgeries()
    {
        try
        {
            var surgery = await appointmentSurgeryService.GetSurgeries();
            return Ok(surgery);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
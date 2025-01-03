public class UpdateAppointmentSurgeryDTO
{

    public string? roomId { get; set; }

    public string? appointmentSurgeryName { get; set; } 

    public DateOnly? appointmentSurgeryDate { get; set; }
    public AppointmentSurgeryStatus? appointmentSurgeryStatus { get; set; }

}
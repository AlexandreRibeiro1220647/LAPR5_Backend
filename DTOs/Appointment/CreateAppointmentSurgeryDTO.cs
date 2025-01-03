public class CreateAppointmentSurgeryDTO
{

    public string roomId { get; set; }

    public string appointmentSurgeryName { get; set; } 

    public string operationRequestId { get; set; }
    public string appointmentSurgeryDate { get; set; }
    public AppointmentSurgeryStatus appointmentSurgeryStatus { get; set; }
    public TimeSpan startTime { get; set; }

    public TimeSpan endTime { get; set; }

}
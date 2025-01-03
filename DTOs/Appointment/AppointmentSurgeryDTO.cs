public class AppointmentSurgeryDTO
{
    public string appointmentSurgeryId { get; set; }
    public string roomId { get; set; }

    public string appointmentSurgeryName { get; set; } 

    public string operationRequestId { get; set; }
    public string appointmentSurgeryDate { get; set; }
    public string appointmentSurgeryStatus { get; set; }
    public TimeSpan startTime { get; set; }

    public TimeSpan endTime { get; set; }


    public AppointmentSurgeryDTO(){
    }
    
    public AppointmentSurgeryDTO(string appointmentSurgeryId, string roomId, string appointmentSurgeryName, string operationRequestId, string appointmentSurgeryDate, string appointmentSurgeryStatus, TimeSpan startTime, TimeSpan endTime)
    {
        this.appointmentSurgeryId = appointmentSurgeryId;
        this.roomId = roomId;
        this.appointmentSurgeryName = appointmentSurgeryName;
        this.operationRequestId = operationRequestId;
        this.appointmentSurgeryDate = appointmentSurgeryDate;
        this.appointmentSurgeryStatus = appointmentSurgeryStatus;
        this.startTime = startTime;
        this.endTime = endTime;
    }
}
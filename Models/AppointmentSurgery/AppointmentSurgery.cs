using NuGet.Packaging.Signing;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;
using TodoApi.Models.Shared;
using TodoApi.Models.Staff;

public class AppointmentSurgery : Entity<AppointmentSurgeryID>       {
    
    public RoomNumber RoomId { get; set; } 

    public AppointmentSurgeryName AppointmentSurgeryName { get; set; } 

    public OperationRequestID OperationRequestID { get; set; }
    
    public AppointmentSurgeryDate AppointmentSurgeryDate {get ; private set;}
    public AppointmentSurgeryStatus AppointmentSurgeryStatus { get; private set; }

    public TimeSpan StartTime {get; set;}
    public TimeSpan EndTime {get; set;}

    
     AppointmentSurgery(){
    }
    public AppointmentSurgery(RoomNumber roomNumber, AppointmentSurgeryName appointmentSurgeryName, OperationRequestID operationRequestID, AppointmentSurgeryDate appointmentSurgeryDate, AppointmentSurgeryStatus appointmentSurgeryStatus, TimeSpan startTime, TimeSpan endTime)
    {
        Id = new AppointmentSurgeryID(Guid.NewGuid().ToString());
        RoomId = roomNumber;
        AppointmentSurgeryName = appointmentSurgeryName;
        OperationRequestID = operationRequestID;
        AppointmentSurgeryDate = appointmentSurgeryDate;
        AppointmentSurgeryStatus = AppointmentSurgeryStatus.SCHEDULED;
        StartTime = startTime;
        EndTime = endTime;
    }

    public AppointmentSurgery(RoomNumber roomNumber, AppointmentSurgeryName appointmentSurgeryName, OperationRequestID operationRequestID, AppointmentSurgeryDate appointmentSurgeryDate, AppointmentSurgeryStatus appointmentSurgeryStatus, TimeSpan startTime, TimeSpan endTime, AppointmentSurgeryID id)
    {
        Id = id;
        RoomId = roomNumber;
        AppointmentSurgeryName = appointmentSurgeryName;
        OperationRequestID = operationRequestID;
        AppointmentSurgeryDate = appointmentSurgeryDate;
        AppointmentSurgeryStatus = AppointmentSurgeryStatus.SCHEDULED;
        StartTime = startTime;
        EndTime = endTime;
    }

    public void UpdateRoom(RoomNumber roomId){
        this.RoomId = roomId;
    }

    public void UpdateAppointmentSurgeryName(AppointmentSurgeryName name) {
        this.AppointmentSurgeryName = name;
    }

    public void UpdateDate(DateOnly date){
        this.AppointmentSurgeryDate = new AppointmentSurgeryDate(date);
    }

    public void UpdateStatus(AppointmentSurgeryStatus newPriority){
        this.AppointmentSurgeryStatus = newPriority;
    }
    
}

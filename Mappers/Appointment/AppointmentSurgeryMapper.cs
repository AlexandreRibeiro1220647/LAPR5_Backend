using TodoApi.Models.OperationRequest;
using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;
using TodoApi.Models.Staff;



public class AppointmentSurgeryMapper : IAppointmentSurgeryMapper
{
  
    public AppointmentSurgeryMapper() {}

     public AppointmentSurgery ToEntity(AppointmentSurgeryDTO dto)
    {
        throw new NotImplementedException();
    }

    public AppointmentSurgeryDTO ToDto(AppointmentSurgery entity)
    {
        return new AppointmentSurgeryDTO(entity.Id.AsString(), entity.RoomId.AsString(), entity.AppointmentSurgeryName.appointmentSurgeryName,
            entity.OperationRequestID.AsString(), entity.AppointmentSurgeryDate.date.ToString(), entity.AppointmentSurgeryStatus.ToString(), entity.StartTime, entity.EndTime );
    }

    public AppointmentSurgery toEntity(CreateAppointmentSurgeryDTO createDto)
    {
        return new AppointmentSurgery(new RoomNumber(createDto.roomId),
            new AppointmentSurgeryName(createDto.appointmentSurgeryName), new OperationRequestID(createDto.operationRequestId),new AppointmentSurgeryDate(DateOnly.Parse(createDto.appointmentSurgeryDate)), createDto.appointmentSurgeryStatus, createDto.startTime, createDto.endTime);
    }

}
using TodoApi.Models.OperationRequest;
using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;
using TodoApi.Models.Staff;



public class SurgeryRoomMapper : ISurgeryRoomMapper
{
  
    public SurgeryRoomMapper() {}

     public SurgeryRoom ToEntity(SurgeryRoomDTO dto)
    {
        throw new NotImplementedException();
    }

    public SurgeryRoomDTO ToDto(SurgeryRoom entity)
    {
        return new SurgeryRoomDTO(entity.Id.AsString(), entity.Capacity.capacity.ToString(), entity.MaintenanceSlots.maintenanceSlots.ToString(),
            entity.RoomName.roomName, entity.RoomStatus.ToString(), entity.RoomTypeId.AsString());
    }

    public SurgeryRoom toEntity(CreateSurgeryRoomDTO createDto)
    {
      throw new NotImplementedException();
    }

}
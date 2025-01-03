using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;
using TodoApi.Models.Shared;
using TodoApi.Models.Staff;

public class SurgeryRoom : Entity<RoomNumber>       {
    
    public Capacity Capacity { get; set; } 
    public MaintenanceSlots MaintenanceSlots { get; set; }

    public RoomName RoomName {get; set; }
    
    public RoomStatus RoomStatus {get ; private set;}
    public RoomTypeId RoomTypeId { get; private set; }

 
     SurgeryRoom(){
    }
    public SurgeryRoom(Capacity capacity, MaintenanceSlots maintenanceSlots , RoomTypeId roomTypeId, RoomName name)
    {
        Id = new RoomNumber(Guid.NewGuid().ToString());
        Capacity = capacity;
        MaintenanceSlots = maintenanceSlots;
        RoomTypeId = roomTypeId;
        RoomName = name;
        RoomStatus = RoomStatus.AVAILABLE;
    }

    public SurgeryRoom(Capacity capacity, MaintenanceSlots maintenanceSlots , RoomTypeId roomTypeId, RoomName name, RoomNumber id)
    {
        Id = id;
        Capacity = capacity;
        MaintenanceSlots = maintenanceSlots;
        RoomTypeId = roomTypeId;
        RoomName = name;
        RoomStatus = RoomStatus.AVAILABLE;
    }


}

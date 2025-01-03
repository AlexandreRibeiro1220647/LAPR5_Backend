
 public class SurgeryRoomDTO
{
    public string roomNumber { get; set; }
    public string capacity { get; set; }
    public string maintenanceSlots { get; set; }
    public string roomName { get; set; }
    public string roomStatus { get; set; }
    public string roomTypeId { get; set; }

    public SurgeryRoomDTO(){
    }
    
    public SurgeryRoomDTO(string roomNumber, string capacity, string maintenanceSlots, string roomName, string roomStatus, string roomTypeId)
    {
        this.roomNumber = roomNumber;
        this.capacity = capacity;
        this.maintenanceSlots = maintenanceSlots;
        this.roomName = roomName;
        this.roomStatus = roomStatus;
        this.roomTypeId = roomTypeId;
    }
}
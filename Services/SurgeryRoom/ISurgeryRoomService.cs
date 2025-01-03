public interface ISurgeryRoomService
{

    Task<List<SurgeryRoomDTO>> GetSurgeryRooms();
    }
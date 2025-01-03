public interface IRoomTypeService
{
    Task<RoomTypeDTO> CreateRoomType(CreateRoomTypeDTO dto);
    
    Task<List<RoomTypeDTO>> GetRoomTypes();
    }
public class RoomTypeMapper : IRoomTypeMapper
{
  
    public RoomTypeMapper() {}

     public RoomType ToEntity(RoomTypeDTO dto)
    {
        throw new NotImplementedException();
    }

    public RoomTypeDTO ToDto(RoomType entity)
    {
        return new RoomTypeDTO(entity.Id.AsString(), entity.RoomDesignation.roomDesignation, entity.RoomDescription.roomDescription
        );
    }

    public RoomType toEntity(CreateRoomTypeDTO createDto)
    {
        return new RoomType(new RoomDesignation(createDto.roomDesignation), new RoomDescription(createDto.roomDescription));
    }

}
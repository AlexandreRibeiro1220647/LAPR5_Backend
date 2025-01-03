public class RoomTypeDTO
{
    public string roomTypeId { get; set; }
    public string designation { get; set; }
    public string roomDescription { get; set; }
 

    public RoomTypeDTO(){
    }
    
    public RoomTypeDTO(string roomTypeId, string designation, string roomDescription)
    {
        this.roomTypeId = roomTypeId;
        this.designation = designation;
        this.roomDescription = roomDescription;
 
    }
}
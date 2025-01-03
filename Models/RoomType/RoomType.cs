using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;
using TodoApi.Models.Shared;
using TodoApi.Models.Staff;

public class RoomType : Entity<RoomTypeId>       {
    
    public RoomDesignation RoomDesignation { get; set; } 

    public RoomDescription RoomDescription { get; set; } 

 
    RoomType(){
    }
    public RoomType(RoomDesignation roomDesignation, RoomDescription roomDescription)
    {
        Id = new RoomTypeId(Guid.NewGuid().ToString());
        RoomDesignation = roomDesignation;
        RoomDescription = roomDescription;
    }

   public RoomType(RoomDesignation roomDesignation, RoomDescription roomDescription, RoomTypeId id)
    {
        Id = id;
        RoomDesignation = roomDesignation;
        RoomDescription = roomDescription;
    }

    

}


using TodoApi.Models.Shared;

public class RoomDesignation {
    public String roomDesignation { get; private set; }
    public RoomDesignation() {}
    
     public RoomDesignation(String roomDesignation) {
        this.roomDesignation = roomDesignation;
    }
    
}
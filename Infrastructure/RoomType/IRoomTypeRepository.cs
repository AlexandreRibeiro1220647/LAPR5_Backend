using TodoApi.Models.Shared;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.Patient;
using TodoApi.Models.OperationType;



public interface IRoomTypeRepository : IRepository<RoomType, RoomTypeId>{
    Task<bool> ExistsAsync(RoomDesignation roomDesignation);

}

    

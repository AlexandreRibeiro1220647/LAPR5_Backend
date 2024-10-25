namespace TodoApi.Services.OperationRequest;

public interface IOperationRequestService
{
    Task<OperationRequestDTO> CreateOperationRequest(CreateOperationRequestDTO dto);
    
   // Task<List<OperationDTO>> GetOperations();
    
    // Task<OperationDTO> UpdateOperationAsync(Guid id, UpdateOperationDTO dto);
    
   //  Task<bool> DeleteOperationRequest(Guid operationId);
}
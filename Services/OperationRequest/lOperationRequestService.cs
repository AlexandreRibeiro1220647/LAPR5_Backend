namespace TodoApi.Services
{

public interface IOperationRequestService
{
    Task<OperationRequestDTO> CreateOperationRequest(CreateOperationRequestDTO dto);
    
    Task<List<OperationRequestDTO>> GetOperations();
    
    Task<List<OperationRequestDTO>> SearchOperations(string? patientName, string? patientId, string? operationType, string? priority, string? deadline);
    Task<OperationRequestDTO> UpdateOperationRequestAsync(Guid id, UpdateOperationRequestDTO dto);
     Task<bool> DeleteOperationRequest(Guid operationId);
}
}
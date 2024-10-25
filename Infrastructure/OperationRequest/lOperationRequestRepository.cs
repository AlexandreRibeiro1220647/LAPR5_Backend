using TodoApi.Models.Shared;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.Patient;
using TodoApi.Models.OperationType;

namespace TodoApi.Infrastructure.OperationRequest; 

public interface IOperationRequestRepository : IRepository<Models.OperationRequest.OperationRequest, OperationRequestID>{
    Task<bool> ExistsAsync(MedicalRecordNumber patientId, OperationTypeID operationTypeID);
    Task<List<Models.OperationRequest.OperationRequest>> SearchAsync(string? patientName, string? patientId, string? operationTypeId, string? priority, string? deadline);

}

    

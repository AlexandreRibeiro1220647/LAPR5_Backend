using TodoApi.Models.Shared;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.Patient;
using TodoApi.Models.OperationType;

namespace TodoApi.Infrastructure.OperationRequest; 

public interface IOperationRequestRepository : IRepository<Models.OperationRequest.OperationRequest, OperationRequestID>{
    Task<Models.OperationRequest.OperationRequest> GetOperationRequestsByLicenseNumberAsync(MedicalRecordNumber medicalRecordNumber);

    Task<bool> ExistsAsync(MedicalRecordNumber patientId, OperationTypeID operationTypeID);

    //Task<Models.OperationRequest.OperationRequest> GetByPatientNameAsync();
    Task<Models.OperationRequest.OperationRequest> GetByOperationTypeAsync(string operationType);

    Task<Models.OperationRequest.OperationRequest> GetByPriorityAsync(string priority);



}

    

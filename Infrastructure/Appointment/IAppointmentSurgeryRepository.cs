using TodoApi.Models.Shared;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.Patient;
using TodoApi.Models.OperationType;


public interface IAppointmentSurgeryRepository : IRepository<AppointmentSurgery, AppointmentSurgeryID>{
    Task<bool> ExistsAsync(OperationRequestID operationRequestID);

}

    

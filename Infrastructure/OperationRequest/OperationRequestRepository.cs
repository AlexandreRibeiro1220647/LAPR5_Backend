using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.Shared;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.Patient;

namespace TodoApi.Infrastructure.OperationRequest;

public class OperationRequestRepository : BaseRepository<Models.OperationRequest.OperationRequest, OperationRequestID>, IOperationRequestRepository
{
    
    private readonly DbSet<Models.OperationRequest.OperationRequest> _dbSet;

    public OperationRequestRepository(IPOContext context) : base(context.OperationRequests)
    {
        _dbSet = context.Set<Models.OperationRequest.OperationRequest>();
    }


    public Task<Models.OperationRequest.OperationRequest> GetOperationRequestsByLicenseNumberAsync(MedicalRecordNumber medicalRecordNumber){

        throw new NotImplementedException();
    }

/*
    public Task<Models.OperationRequest.OperationRequest> GetByPatientNameAsync (){
        throw new NotImplementedException();
    } 
*/

     public Task<Models.OperationRequest.OperationRequest> GetByOperationTypeAsync(string operationType) {
        throw new NotImplementedException();
    }

    public Task<Models.OperationRequest.OperationRequest> GetByPriorityAsync(string priority){
        throw new NotImplementedException();
    }


}
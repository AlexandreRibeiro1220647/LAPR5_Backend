using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.Patient;
using TodoApi.Infrastructure.Shared;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;

namespace TodoApi.Infrastructure.OperationRequest;

public class OperationRequestRepository : BaseRepository<Models.OperationRequest.OperationRequest, OperationRequestID>, IOperationRequestRepository
{
    
    private readonly DbSet<Models.OperationRequest.OperationRequest> _dbSet;

    private readonly IPatientRepository _patientRepository;

    public OperationRequestRepository(IPOContext context) : base(context.OperationRequests)
    {
        _dbSet = context.Set<Models.OperationRequest.OperationRequest>();
    }

    public async Task<bool> ExistsAsync(MedicalRecordNumber patientId, OperationTypeID operationTypeID)
{
    return await _dbSet.AnyAsync(req => req.PacientId == patientId && req.OperationTypeID == operationTypeID);
}   

public async Task<List<Models.OperationRequest.OperationRequest>> SearchAsync(string? patientName, string? patientId, string? operationTypeId, string? priority, string? deadline)
{

    IQueryable<Models.OperationRequest.OperationRequest> query = _dbSet;
    
    if(!string.IsNullOrEmpty(patientName)){
        
        var patients = await _patientRepository.GetByNameAsync(patientName);

        var patientIds = patients.Select(p => p.Id).ToList();

        if (patientIds.Any())
        {
            query = query.Where(op => patientIds.Contains(op.PacientId));
        }
    } 

    if (!string.IsNullOrEmpty(operationTypeId))
    {
        query = query.Where(op => op.OperationTypeID.Equals(operationTypeId));
    }

    if (!string.IsNullOrEmpty(priority))
    {
        query = query.Where(op => op.Priority.ToString().Equals(priority, StringComparison.OrdinalIgnoreCase));
    }

    if (!string.IsNullOrEmpty(patientId))
    {
        query = query.Where(op => op.PacientId.Equals(patientId));
    }
    
    if (!string.IsNullOrEmpty(deadline) && DateOnly.TryParse(deadline, out var deadlineDate))
    {
        query = query.Where(op => op.Deadline.deadline <= deadlineDate);
    }

    return await query.ToListAsync();
}


}
using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.OperationType;
using TodoApi.Infrastructure.Patient;
using TodoApi.Infrastructure.Shared;
using TodoApi.Models;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;

namespace TodoApi.Infrastructure.OperationRequest;

public class OperationRequestRepository : BaseRepository<Models.OperationRequest.OperationRequest, OperationRequestID>, IOperationRequestRepository
{
    
    private readonly DbSet<Models.OperationRequest.OperationRequest> _dbSet;

    private readonly IPatientRepository _patientRepository;

    private readonly IOperationTypeRepository _operationTypeRepository;

    public OperationRequestRepository(IPOContext context, IPatientRepository patientRepository, IOperationTypeRepository operationTypeRepository) : base(context.OperationRequests)
    {
        _dbSet = context.Set<Models.OperationRequest.OperationRequest>();
        _patientRepository=patientRepository;
        _operationTypeRepository = operationTypeRepository;
    }

    public async Task<bool> ExistsAsync(MedicalRecordNumber patientId, OperationTypeID operationTypeID)
{
    return await _dbSet.AnyAsync(req => req.PacientId == patientId && req.OperationTypeID == operationTypeID);
}   

public async Task<List<Models.OperationRequest.OperationRequest>> SearchAsync(string? patientName, string? patientId, string? operationTypeName, string? priority, string? deadline)
{

    IQueryable<Models.OperationRequest.OperationRequest> query = _dbSet;
    
    if(!string.IsNullOrEmpty(patientName)){
        
        var patients = await _patientRepository.GetByNameAsync(patientName);

        var patientIds = patients.Select(p => p.Id).ToList();

        if (patientIds.Any())
        {
            query = query.Where(op => patientIds.Contains(op.PacientId));
        }
        else
        {
            return new List<Models.OperationRequest.OperationRequest>();
        }
    }       

    if (!string.IsNullOrEmpty(operationTypeName))
    {
        
        var operationsType = await _operationTypeRepository.GetByNameAsync(operationTypeName);;
        
        if (operationsType != null)
    {
        var operationTypeIdParsed = new OperationTypeID(operationsType.Id.ToString());

        query = query.Where(op => op.OperationTypeID.Equals(operationTypeIdParsed));
    }
    else
    {
            return new List<Models.OperationRequest.OperationRequest>();
    }
    }

    if (!string.IsNullOrEmpty(priority))
    {
    if (Enum.TryParse<Priority>(priority, true, out var priorityEnum))
    {
        query = query.Where(op => op.Priority == priorityEnum);
    }
    }

    if (!string.IsNullOrEmpty(patientId))
    {
        query = query.Where(op => op.PacientId.Equals(new MedicalRecordNumber(patientId)));
    }
          if (!string.IsNullOrEmpty(deadline) && DateOnly.TryParse(deadline, out var deadlineDate))
          {

          query = query.Where(op => op.Deadline.Equals(new Deadline(deadlineDate)));
          }


    return await query.ToListAsync();
}
}


using TodoApi.Infrastructure;
using TodoApi.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.Patient;
using TodoApi.Infrastructure.Shared;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;
using TodoApi.Models;

namespace TodoApi.Infrastructure.OperationRequestLog;

public class OperationRequestLogRepository : BaseRepository<Models.RequestsLog, OperationRequestLogID>, IOperationRequestLogRepository

{
    private readonly DbSet<RequestsLog> _dbSet;

    public OperationRequestLogRepository(IPOContext context) : base(context.requestsLogs)
    {
        _dbSet = context.Set<RequestsLog>();
    }
    
}
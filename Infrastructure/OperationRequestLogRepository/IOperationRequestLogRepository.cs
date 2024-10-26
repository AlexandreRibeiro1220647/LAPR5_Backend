
using Microsoft.EntityFrameworkCore.Infrastructure;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.Shared;

namespace TodoApi.Infrastructure; 

public interface IOperationRequestLogRepository : IRepository<Models.RequestsLog, OperationRequestLogID>{
    
}
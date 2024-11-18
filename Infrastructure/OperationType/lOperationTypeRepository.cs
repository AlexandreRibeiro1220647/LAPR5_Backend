

using TodoApi.Models.OperationType;
using TodoApi.Models.Shared;

namespace TodoApi.Infrastructure.OperationType;
public interface IOperationTypeRepository : IRepository<Models.OperationType.OperationType, OperationTypeID>
{
    Task<Models.OperationType.OperationType?> GetByNameAsync(string name);
    Task<List<Models.OperationType.OperationType>> SearchAsync(string? name, string? specialization, string? estimatedDuration, string? status);
}
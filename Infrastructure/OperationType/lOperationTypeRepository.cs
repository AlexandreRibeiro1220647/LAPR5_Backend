

using TodoApi.Models.OperationType;
using TodoApi.Models.Shared;

namespace TodoApi.Infrastructure.OperationType;
public interface IOperationTypeRepository : IRepository<Models.OperationType.OperationType, OperationTypeID>
{
    Task<Models.OperationType.OperationType?> GetByNameAsync(string name);
    Task<List<Models.OperationType.OperationType>> SearchByName(string name);
    Task<List<Models.OperationType.OperationType>> SearchBySpecialization(string specialization);
    Task<List<Models.OperationType.OperationType>> SearchByStatus(bool status);
}
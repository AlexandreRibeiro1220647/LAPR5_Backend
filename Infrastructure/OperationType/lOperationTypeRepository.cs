

using TodoApi.Models.OperationType;
using TodoApi.Models.Shared;

namespace TodoApi.Infrastructure.OperationType;
public interface IOperationTypeRepository : IRepository<Models.OperationType.OperationType, OperationTypeID>
{
    
}
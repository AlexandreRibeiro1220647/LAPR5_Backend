
using TodoApi.DTOs.OperationType;

namespace TodoApi.Mappers;
public interface IOperationTypeMapper : IMapper<Models.OperationType.OperationType, OperationTypeDTO,CreateOperationTypeDTO>
{
    
}
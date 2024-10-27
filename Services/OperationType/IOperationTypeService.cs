

using TodoApi.DTOs.OperationType;

namespace TodoApi.Services.OperationType;

public interface IOperationTypeService
{

    Task<OperationTypeDTO> CreateOperationType(CreateOperationTypeDTO operationTypeDto);
    Task<OperationTypeDTO> UpdateOperationTypeAsync(Guid id, UpdateOperationTypeDTO dto);
    Task<OperationTypeDTO> DeleteOperationType(Guid id);
    Task<List<OperationTypeDTO>> GetOperationTypes();
    Task<List<OperationTypeDTO>> GetOperationTypesBySpecialization(string specialization);
    Task<List<OperationTypeDTO>> GetOperationTypesByName(string name);
    Task<List<OperationTypeDTO>> GetOperationTypesByStatus(bool status);

}
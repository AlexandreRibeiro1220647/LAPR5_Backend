

using TodoApi.DTOs.OperationType;

namespace TodoApi.Services.OperationType;

public interface IOperationTypeService
{
    Task<OperationTypeDTO> CreateOperationType(CreateOperationTypeDTO operationTypeDto);
    Task<OperationTypeDTO> UpdateOperationTypeAsync(Guid id, UpdateOperationTypeDTO dto);
    Task<OperationTypeDTO> DeleteOperationType(Guid id);
    Task<List<OperationTypeDTO>> GetOperationTypes();
    Task<List<OperationTypeDTO>> SearchOperationTypes(string? name, string? specialization, string? estimatedDuration, string? status);
}
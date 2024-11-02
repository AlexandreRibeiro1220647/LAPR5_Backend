


using TodoApi.DTOs.OperationType;
using TodoApi.Infrastructure.OperationType;
using TodoApi.Mappers.OperationType;
using TodoApi.Models.Shared;
using TodoApi.Services.OperationType;
using TodoApi.Models.OperationType;

namespace TodoApi.Services;

public class OperationTypeService : IOperationTypeService
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOperationTypeRepository _operationTypeRepository;
    private readonly ILogger<IPatientService> _logger;
    private OperationTypeMapper _mapper = new OperationTypeMapper();

    public OperationTypeService(IUnitOfWork unitOfWork, IOperationTypeRepository operationTypeRepository, ILogger<IPatientService> logger)
    {
        this._unitOfWork = unitOfWork;
        this._operationTypeRepository = operationTypeRepository;
        this._logger = logger;
    }
    
    
    
    public async Task<OperationTypeDTO> CreateOperationType(CreateOperationTypeDTO createOperationTypeDto)
    {
        try
        {
            // Check if an operation type with the same name already exists
            var existingOperationType = await _operationTypeRepository.GetByNameAsync(createOperationTypeDto.Name);
            if (existingOperationType != null)
            {
                throw new Exception("An operation type with the same name already exists.");
            }

            // Map the DTO to the entity
            Models.OperationType.OperationType mapped = _mapper.toEntity(createOperationTypeDto);

            // Add the new operation type to the repository
            await this._operationTypeRepository.AddAsync(mapped);

            // Map the entity back to a DTO
            OperationTypeDTO mappedDto = _mapper.ToDto(mapped);

            // Commit the changes
            await this._unitOfWork.CommitAsync();

            return mappedDto;
        }
        catch (Exception e)
        {
            this._logger.LogError(e, "Error creating operation type");
            throw;
        }
    }


        public async Task<OperationTypeDTO> UpdateOperationTypeAsync(Guid id, UpdateOperationTypeDTO dto)
    {
        try
        {
            
            Models.OperationType.OperationType existingOperationType = await _operationTypeRepository.GetByIdAsync(new OperationTypeID(id));

            if (existingOperationType == null)
            {
                throw new Exception("OperationRequest not found");
            }

        if (!dto.Name.Equals(""))
        {
            existingOperationType.UpdateName(dto.Name);
        }

        if (dto.EstimatedDuration.HasValue)
        {
            existingOperationType.UpdateEstimatedDuration(dto.EstimatedDuration.Value);
        }

        if (dto.RequiredStaffBySpecialization.Count != 0)
        {
            existingOperationType.UpdateRequiredStaffBySpecialization(dto.RequiredStaffBySpecialization);
        }

            // Save the changes
            await _unitOfWork.CommitAsync();

            // Create the updated DTO with the required parameters in the correct order
            OperationTypeDTO updatedOperationTypeDto = new OperationTypeDTO(
                existingOperationType.Name,
                existingOperationType.RequiredStaffBySpecialization,
                existingOperationType.EstimatedDuration,
                existingOperationType.Id.AsString(),
                existingOperationType.IsActive
            );

            return updatedOperationTypeDto;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating operation type");
            throw;
        }
    }


    public async Task<OperationTypeDTO> DeleteOperationType(Guid id)
    {
        try
        {
            
            Models.OperationType.OperationType existingOperationType = await _operationTypeRepository.GetByIdAsync(new OperationTypeID(id));

            if (existingOperationType == null)
            {
                throw new Exception("OperationRequest not found");
            }

            existingOperationType.Delete();

            // Save the changes
            await _unitOfWork.CommitAsync();

            // Create the updated DTO with the required parameters in the correct order
            OperationTypeDTO updatedOperationTypeDto = new OperationTypeDTO(
                existingOperationType.Name,
                existingOperationType.RequiredStaffBySpecialization,
                existingOperationType.EstimatedDuration,
                existingOperationType.Id.AsString(),
                existingOperationType.IsActive
            );

            return updatedOperationTypeDto;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating operation type");
            throw;
        }
    }

    public async Task<List<OperationTypeDTO>> GetOperationTypes()
    {
        List<Models.OperationType.OperationType> operationTypes = await _operationTypeRepository.GetAllAsync();
        return operationTypes.Select(operationType => _mapper.ToDto(operationType)).ToList();
    }

    public async Task<List<OperationTypeDTO>> GetOperationTypesBySpecialization(string specialization)
    {
    try
        {
            var operationTypes = await _operationTypeRepository.SearchBySpecialization(specialization);
            return operationTypes.Select(operationType => _mapper.ToDto(operationType)).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error searching staff by specialization");
            throw;
        }
    }

    public async Task<List<OperationTypeDTO>> GetOperationTypesByName(string name)
    {
    try
        {
            var operationTypes = await _operationTypeRepository.SearchByName(name);
            return operationTypes.Select(operationType => _mapper.ToDto(operationType)).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error searching staff by specialization");
            throw;
        }
    }

    public async Task<List<OperationTypeDTO>> GetOperationTypesByStatus(bool status)
    {
    try
        {
            var operationTypes = await _operationTypeRepository.SearchByStatus(status);
            return operationTypes.Select(operationType => _mapper.ToDto(operationType)).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error searching staff by specialization");
            throw;
        }
    }
}
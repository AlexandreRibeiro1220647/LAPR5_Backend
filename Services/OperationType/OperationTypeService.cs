


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
    private readonly IConfiguration _config;
    private OperationTypeMapper _mapper = new OperationTypeMapper();

    public OperationTypeService(IUnitOfWork unitOfWork, IOperationTypeRepository operationTypeRepository, ILogger<IPatientService> logger,
        IConfiguration config)
    {
        this._unitOfWork = unitOfWork;
        this._operationTypeRepository = operationTypeRepository;
        this._logger = logger;
        this._config = config;
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
                existingOperationType.Id.AsString()
            );

            return updatedOperationTypeDto;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating operation type");
            throw;
        }
    }

}
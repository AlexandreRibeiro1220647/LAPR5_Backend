using TodoApi.Models.OperationType;
using TodoApi.Models.Shared;
using TodoApi.DTOs;
using TodoApi.DTOs.OperationType;
using TodoApi.Infrastructure.OperationType;
using TodoApi.Infrastructure.OperationRequest;
using TodoApi.Mappers;
using TodoApi.Mappers.OperationType;
using TodoApi.Mappers.OperationRequest;
using TodoApi.Services;

namespace TodoApi.Services;

public class PlanningService : IPlanningService
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<IPlanningService> _logger;

    private readonly IOperationTypeRepository _operationTypeRepository;
    private OperationTypeMapper _mapperOpType = new OperationTypeMapper();

    private readonly IOperationRequestRepository _operationRequestRepository;
    private OperationRequestMapper _mapperOpRequest = new OperationRequestMapper();


    public PlanningService(IUnitOfWork unitOfWork, IOperationTypeRepository operationTypeRepository, ILogger<IPlanningService> logger, IOperationRequestRepository operationRepository)
    {
        this._unitOfWork = unitOfWork;
        this._operationTypeRepository = operationTypeRepository;
        this._logger = logger;
        this._operationRequestRepository = operationRepository;
    }
    
public async Task<List<OperationTypeDurationDTO>> GetOperationTypeDurations()
{
    // Fetch all operation types from the repository
    List<Models.OperationType.OperationType> operationTypes = await _operationTypeRepository.GetAllAsync();

    // Map to OperationTypeDurationDTO
    return operationTypes.Select(operationType =>
    {
        // Extract the estimated durations for anesthesia, surgery, and cleaning
        var durations = _mapperOpType.ToDto(operationType).EstimatedDuration;

        // Ensure there are at least three durations for the mapping
        if (durations.Count < 3)
        {
            throw new InvalidOperationException($"OperationType {operationType.Id} does not have sufficient duration data. - {durations[0]} , {durations[1]}");
        }

        return new OperationTypeDurationDTO(
            operationType.Id.AsString(),
            durations[0],  // Anesthesia duration
            durations[1],  // Surgery duration
            durations[2]   // Cleaning duration
        );
    }).ToList();
}

    
    public async Task<List<OperationRequestTypeDTO>> GetOperationRequestTypes()
    {
        try
        {
            List<Models.OperationRequest.OperationRequest> requests = await _operationRequestRepository.GetAllAsync();
            List<OperationRequestDTO> requestsDTO = new List<OperationRequestDTO>();
            foreach (Models.OperationRequest.OperationRequest operation in requests)
            {
                requestsDTO.Add(_mapperOpRequest.ToDto(operation));
            }

            List<OperationRequestTypeDTO> result = new List<OperationRequestTypeDTO>();

            foreach (OperationRequestDTO opR in requestsDTO)
            {
                result.Add(new OperationRequestTypeDTO(opR.operationId, opR.operationTypeId));
            }

            return result;
        }
        catch (Exception e)
        {
            this._logger.LogError(e, "Error getting operation request - type relation");
            throw;
        }
    }
}
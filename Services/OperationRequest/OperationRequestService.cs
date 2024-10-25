using Microsoft.AspNetCore.Mvc;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.OperationRequest;
using TodoApi.Mappers.OperationRequest;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;
using TodoApi.Models.Shared;

namespace TodoApi.Services.OperationRequest;

public class OperationService : IOperationRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOperationRequestRepository _operationRequestRepository;

    //private readonly IBackOfficeUserRepository _userRepository;
    private readonly ILogger<IOperationRequestService> _logger;
    private readonly IConfiguration _config;
    private OperationRequestMapper mapper = new OperationRequestMapper();

    private readonly IHttpContextAccessor _httpContextAccessor;

    public OperationService(IUnitOfWork unitOfWork, IOperationRequestRepository operationRepository, ILogger<IOperationRequestService> logger,
        IConfiguration config, IHttpContextAccessor httpContextAccessor/*/, IUserRepository userRepository*/)
    {
        this._unitOfWork = unitOfWork;
        this._operationRequestRepository = operationRepository;
        this._logger = logger;
        this._config = config;
        //this._userRepository = userRepository;
        this._httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationRequestDTO> CreateOperationRequest(CreateOperationRequestDTO dto)
{
    try
    {
        // Ir buscar o doctorID da sessão

        // validates that the operation type matches the doctor’s specialization!!!!

        // Verifica se o paciente existe
        /*bool patientExists = await _patientRepository.ExistsAsync(dto.pacientid);
        if (!patientExists)
        {
            throw new Exception("The specified patient does not exist.");
        }

        */        
        // Verifica duplicação
        var patientIdd = new MedicalRecordNumber(dto.pacientid);
        var operationTypeIdd = new OperationTypeID(dto.operationTypeId);
        bool operationExists = await _operationRequestRepository.ExistsAsync(patientIdd, operationTypeIdd);
        if (operationExists)
        {
            throw new Exception("An operation request for this patient and operation type already exists.");
        }

       Models.OperationRequest.OperationRequest mapped = mapper.toEntity(dto);
            
            await this._operationRequestRepository.AddAsync(mapped);
            
            OperationRequestDTO mappedDto = mapper.ToDto(mapped);
            
            await this._unitOfWork.CommitAsync();
            
            return mappedDto;
        }
        catch (Exception e)
        {
            this._logger.LogError(e, "Error registering operation");
            throw;
        }
}

    public async Task<bool> DeleteOperationRequest(Guid operationRequestId)
    {
        try
        {

            // operationRequests que o proprio doutor criou

           Models.OperationRequest.OperationRequest operationRequest = await _operationRequestRepository.GetByIdAsync(new OperationRequestID(operationRequestId));

            if (operationRequest == null)
            {
            _logger.LogWarning($"Operation request with ID {operationRequestId} not found.");
            throw new Exception("Operation request not found.");
            }

             // Verifica se a operação já foi agendada com base no prazo (Deadline)
            if (!string.IsNullOrEmpty(operationRequest.Deadline.ToString()) && DateOnly.TryParse(operationRequest.Deadline.ToString(), out var deadline) && deadline < DateOnly.FromDateTime(DateTime.Now))
            {
                _logger.LogWarning($"Operation with id {operationRequestId} has already been scheduled.");
                throw new Exception("Operation has already been scheduled and cannot be deleted.");
            }
        

            bool confirmDeletion = ConfirmDeletion();
            if (!confirmDeletion)
            {
                _logger.LogInformation($"Deletion of OperationRequest with id {operationRequestId} was cancelled.");
                return false;
            }
            _operationRequestRepository.Remove(operationRequest);
            
            await _unitOfWork.CommitAsync();

            _logger.LogInformation($"OperationRequest with id {operationRequestId} was successfully deleted.");

            return true;
            
        }catch (Exception e)
        {
            this._logger.LogError(e, "Error deleting operationRequest");
            throw;
        }
    }

    private bool ConfirmDeletion()
        {
            // This method should prompt the admin to confirm the deletion.
            // For simplicity, we'll assume the admin confirms the deletion.
            // In a real application, this would involve a UI prompt.
            return true;
        }
}

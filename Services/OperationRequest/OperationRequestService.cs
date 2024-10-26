using Microsoft.AspNetCore.Mvc;
using TodoApi.Infrastructure;
using TodoApi.Infrastructure.OperationRequest;
using TodoApi.Infrastructure.Patient;
using TodoApi.Infrastructure.Staff;
using TodoApi.Mappers.OperationRequest;
using TodoApi.Models;
using TodoApi.Models.OperationRequest;
using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;
using TodoApi.Models.Shared;
using TodoApi.Models.Staff;

namespace TodoApi.Services
{

public class OperationRequestService : IOperationRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOperationRequestRepository _operationRequestRepository;

    private readonly IPatientRepository _patientRepository;

    private readonly IStaffRepository _staffRepository;

    //private readonly IBackOfficeUserRepository _userRepository;

    //private readonly IOperationRequestLogRepository _operationRequestLogRepository;
    private readonly ILogger<IOperationRequestService> _logger;
    private readonly IConfiguration _config;
    private OperationRequestMapper mapper = new OperationRequestMapper();

    public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository operationRepository, ILogger<IOperationRequestService> logger,
        IConfiguration config, IPatientRepository patientRepository, IOperationRequestLogRepository operationRequestLogRepository, IStaffRepository staffRepository/*, IUserRepository userRepository*/)
    {
        this._unitOfWork = unitOfWork;
        this._operationRequestRepository = operationRepository;
        this._logger = logger;
        this._config = config;
        //this._userRepository = userRepository;
        this._patientRepository = patientRepository;
       // this._operationRequestLogRepository = operationRequestLogRepository;
        this._staffRepository = staffRepository;
    }

    public async Task<OperationRequestDTO> CreateOperationRequest(CreateOperationRequestDTO dto)
{
    try
    {
        // Ir buscar o doctorID da sessão

        var doctorId = new LicenseNumber(dto.doctorid);
        bool doctorExists = await _staffRepository.ExistsAsync(doctorId);
        if (!doctorExists)
        {
            throw new Exception("The specified doctor does not exist.");
        }

        if (!int.TryParse(dto.operationTypeId, out int operationTypeId))
        {
            throw new Exception("Invalid operation type ID.");
        }

        // Verifica a especialização do médico se o OperationTypeID for entre 1 e 10
        if (operationTypeId >= 1 && operationTypeId <= 10)
        {
            var doctor = await _staffRepository.GetByIdAsync(doctorId);
            if (!doctor.Specialization.Equals("Orthopedics"))
            {
                throw new Exception("The doctor must have a specialization in Orthopedics for this operation type.");
            }
        }
        // Verifica se o paciente existe
        bool patientExists = await _patientRepository.ExistsAsync(new MedicalRecordNumber(dto.pacientid));
        if (!patientExists)
        {
            throw new Exception("The specified patient does not exist.");
        }      
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

    public async Task<OperationRequestDTO> UpdateOperationRequestAsync(Guid id, UpdateOperationRequestDTO dto)
    {
        try
        {
            
            Models.OperationRequest.OperationRequest existingOperationRequest = await _operationRequestRepository.GetByIdAsync(new OperationRequestID(id));

            if (existingOperationRequest == null)
            {
                throw new Exception("OperationRequest not found");
            }

          //  List<RequestsLog> logs = new List<RequestsLog>();

        if (dto.Deadline.HasValue)
        {
            existingOperationRequest.UpdateDeadline(dto.Deadline.Value);
         /*   logs.Add(new RequestsLog{OperationRequestId = new OperationRequestID(id),
                ChangeDate = DateTime.UtcNow,
                ChangeDescription = $"Deadline updated to {dto.Deadline.Value:yyyy-MM-dd}"
            });*/
        }

        if (dto.Priority.HasValue)
        {
            existingOperationRequest.UpdatePriority(dto.Priority.Value);
       /*     logs.Add(new RequestsLog
            {
                OperationRequestId = new OperationRequestID(id),
                ChangeDate = DateTime.UtcNow,
                ChangeDescription = $"Priority updated to {dto.Priority.Value}"
            });*/
        }
/*
        if (logs.Any())
        {
            // Salva os logs no repositório de logs
            foreach (var log in logs)
            {
                await _operationRequestLogRepository.AddAsync(log);
            }
        }
*/
            // Save the changes
            await _unitOfWork.CommitAsync();

            // Create the updated DTO with the required parameters in the correct order
            OperationRequestDTO updatedOperationRequestDto = new OperationRequestDTO(
                existingOperationRequest.OperationTypeID.AsString(),
                existingOperationRequest.PacientId.AsString(),
                existingOperationRequest.DoctorId.AsString(),
                existingOperationRequest.OperationTypeID.AsString(),
                existingOperationRequest.Priority.ToString(),
                existingOperationRequest.Deadline.deadline.ToString("yyyy-MM-dd")

            );

            return updatedOperationRequestDto;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating patient");
            throw;
        }
    }

    public async Task<List<OperationRequestDTO>> GetOperations()
    {
        try
        {
            List<Models.OperationRequest.OperationRequest> operations = await _operationRequestRepository.GetAllAsync();
            List<OperationRequestDTO> dtos = new List<OperationRequestDTO>();
            foreach (Models.OperationRequest.OperationRequest operation in operations)
            {
                dtos.Add(mapper.ToDto(operation));
            }

            return dtos;
        }
        catch (Exception e)
        {
            this._logger.LogError(e, "Error getting operations");
            throw;
        }
    }

    public async Task<List<OperationRequestDTO>> SearchOperations(string? patientName,string? patientId, string? operationType, string? priority, string? deadline)
    {
        try
        {
        // Call the repository to get filtered data based on parameters
        List<Models.OperationRequest.OperationRequest> operations = await _operationRequestRepository.SearchAsync(patientName, patientId, operationType, priority, deadline);

        List<OperationRequestDTO> dtos = operations.Select(op => mapper.ToDto(op)).ToList();

        return dtos;
    }
    catch (Exception e)
    {
        this._logger.LogError(e, "Error searching operations");
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
}
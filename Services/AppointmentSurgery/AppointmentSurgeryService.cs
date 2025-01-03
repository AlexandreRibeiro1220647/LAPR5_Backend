using Microsoft.AspNetCore.Mvc;
using Npgsql.Internal;
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

public class AppointmentSurgeryService : IAppointmentSurgeryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppointmentSurgeryRepository appointmentSurgeryRepository;

    private readonly ILogger<IAppointmentSurgeryService> _logger;
    private readonly IConfiguration _config;
    private AppointmentSurgeryMapper mapper = new AppointmentSurgeryMapper();

    public AppointmentSurgeryService(IUnitOfWork unitOfWork, IAppointmentSurgeryRepository appointmentSurgeryRepository, ILogger<IAppointmentSurgeryService> logger,
        IConfiguration config )
    {
        this._unitOfWork = unitOfWork;
        this.appointmentSurgeryRepository = appointmentSurgeryRepository;
        this._logger = logger;
        this._config = config;
    }

    public async Task<AppointmentSurgeryDTO> CreateAppointmentSurgery(CreateAppointmentSurgeryDTO dto)
{
    try
    {
        
        var operationId = new OperationRequestID(dto.operationRequestId);
        bool appointmentExists = await appointmentSurgeryRepository.ExistsAsync(operationId);
        if (appointmentExists)
        {
            throw new Exception("An AppointmentSurgery for this operation Request already exists.");
        }
 
            AppointmentSurgery mapped = mapper.toEntity(dto);
            
            await this.appointmentSurgeryRepository.AddAsync(mapped);
            
            AppointmentSurgeryDTO mappedDto = mapper.ToDto(mapped);
            
            await this._unitOfWork.CommitAsync();
            
            return mappedDto;
        }
        catch (Exception e)
        {
        // Captura a exceção interna, se houver
            var innerExceptionMessage = e.InnerException != null ? e.InnerException.Message : "No inner exception";

             _logger.LogError(e, $"Error saving entity changes. Inner Exception: {innerExceptionMessage}");
             throw new Exception($"Error saving entity changes. Details: {e.Message} | Inner Exception: {innerExceptionMessage}", e);

            }

}

    public async Task<AppointmentSurgeryDTO> UpdateAppointmentSurgeryAsync(Guid id, UpdateAppointmentSurgeryDTO dto)
    {
        try
        {
            
            AppointmentSurgery existingAppointmentSurgery = await appointmentSurgeryRepository.GetByIdAsync(new AppointmentSurgeryID(id));

            if (existingAppointmentSurgery == null)
            {
                throw new Exception("AppointmentSurgery not found");
            }

        if (!string.IsNullOrEmpty(dto.roomId))       
         {
            existingAppointmentSurgery.UpdateRoom(new RoomNumber(dto.roomId));

        }

       if (!string.IsNullOrEmpty(dto.appointmentSurgeryName))
        {
            existingAppointmentSurgery.UpdateAppointmentSurgeryName(new AppointmentSurgeryName(dto.appointmentSurgeryName));
        }

        // Atualiza AppointmentSurgeryDate, se fornecido
        if (dto.appointmentSurgeryDate.HasValue)
        {
            existingAppointmentSurgery.UpdateDate(dto.appointmentSurgeryDate.Value);
        }

        // Atualiza AppointmentSurgeryStatus, se fornecido
        if (dto.appointmentSurgeryStatus.HasValue)
        {
            existingAppointmentSurgery.UpdateStatus(dto.appointmentSurgeryStatus.Value);
        }

            // Save the changes
            await _unitOfWork.CommitAsync();

            // Create the updated DTO with the required parameters in the correct order
             var updatedAppointmentSurgeryDto = new AppointmentSurgeryDTO(
            existingAppointmentSurgery.Id.AsString(),
            existingAppointmentSurgery.RoomId.AsString(),
            existingAppointmentSurgery.AppointmentSurgeryName.appointmentSurgeryName,
            existingAppointmentSurgery.OperationRequestID.AsString(),
            existingAppointmentSurgery.AppointmentSurgeryDate.date.ToString("yyyy-MM-dd"),
            existingAppointmentSurgery.AppointmentSurgeryStatus.ToString(),
            existingAppointmentSurgery.StartTime,
            existingAppointmentSurgery.EndTime
        );
            return updatedAppointmentSurgeryDto;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating AppointmentSurgery");
            throw;
        }
    }

    public async Task<List<AppointmentSurgeryDTO>> GetSurgeries()
    {
        try
        {
            List<AppointmentSurgery> surgeries = await appointmentSurgeryRepository.GetAllAsync();
            List<AppointmentSurgeryDTO> dtos = new List<AppointmentSurgeryDTO>();
            foreach (AppointmentSurgery surgery in surgeries)
            {
                dtos.Add(mapper.ToDto(surgery));
            }

            return dtos;
        }
        catch (Exception e)
        {
            this._logger.LogError(e, "Error getting surgeries");
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
public interface IAppointmentSurgeryService
{
    Task<AppointmentSurgeryDTO> CreateAppointmentSurgery(CreateAppointmentSurgeryDTO dto);
    
    Task<List<AppointmentSurgeryDTO>> GetSurgeries();
    
    Task<AppointmentSurgeryDTO> UpdateAppointmentSurgeryAsync(Guid id, UpdateAppointmentSurgeryDTO dto);
    
}

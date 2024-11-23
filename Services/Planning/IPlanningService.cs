using TodoApi.DTOs;

namespace TodoApi.Services;

public interface IPlanningService
{
    Task<List<OperationTypeDurationDTO>> GetOperationTypeDurations();
    Task<List<OperationRequestTypeDTO>> GetOperationRequestTypes();
    Task<List<OperationRequestDoctorDTO>> GetOperationRequestDoctors();
}
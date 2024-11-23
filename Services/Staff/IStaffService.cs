using TodoApi.Models.Staff;
using TodoApi.DTOs;

namespace TodoApi.Services
{
    public interface IStaffService
    {
        Task<StaffDTO> CreateStaff(CreateStaffDTO dto);
        Task<List<StaffDTO>> GetStaff();
        Task<StaffDTO> UpdateStaff(Guid id, UpdateStaffDTO dto);
        Task<StaffDTO> InactivateStaff(Guid id, UpdateStaffDTO dto);
        Task<List<StaffDTO>> SearchStaff(
                    string? fullName,
                    string? specialization,
                    string? email,
                    string? status,
                    string? phone);
    }
}
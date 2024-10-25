using TodoApi.Models.Staff;
using TodoApi.DTOs;

namespace TodoApi.Services
{
    public interface IStaffService
    {
        Task<StaffDTO> CreateStaff(CreateStaffDTO dto);
        Task<List<StaffDTO>> GetStaff();
        Task<StaffDTO> UpdateStaff(Guid id, UpdateStaffDTO dto);
        Task<StaffDTO> UpdateStaffStatus(Guid id, UpdateStaffDTO dto);
        Task<List<StaffDTO>> GetStaffByName(string name);
        Task<List<StaffDTO>> GetStaffByEmail(string email);
        Task<List<StaffDTO>> GetStaffBySpecialization(string specialization);

    }
}
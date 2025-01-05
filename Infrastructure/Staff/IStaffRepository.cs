using TodoApi.Models.Shared;
using TodoApi.Models.Staff;
using TodoApi.DTOs.Staff;


namespace TodoApi.Infrastructure.Staff
{
    public interface IStaffRepository : IRepository<Models.Staff.Staff, LicenseNumber>
    {
        Task<Models.Staff.Staff> GetByLicenseNumber(LicenseNumber licenseNumber);
        Task<bool> ExistsAsync(LicenseNumber staffId);
        Task<Models.Staff.Staff> GetByPhoneAsync(string phone);
        Task<List<Models.Staff.Staff>> SearchAsync(
            string? fullName = null,
            string? specialization = null,
            string? email = null,
            string? status = null,
            string? phone = null);
        Task<List<Models.Staff.Staff>> SearchBySpecialization(StaffSearchDto searchDto);
        Task<List<Models.Staff.Staff>> SearchByStatus(StaffStatus status);
        Task<List<Models.Staff.Staff>> GetByUserAsync(TodoApi.DTOs.User.UserDTO user);
    }
}
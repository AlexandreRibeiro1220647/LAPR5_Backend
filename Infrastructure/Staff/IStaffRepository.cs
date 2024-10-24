using TodoApi.Models.Shared;
using TodoApi.Models.Staff;

namespace TodoApi.Infrastructure.Staff
{
    public interface IStaffRepository : IRepository<Models.Staff.Staff, LicenseNumber>
    {
        Task<Models.Staff.Staff> GetByLicenseNumberAsync(LicenseNumber licenseNumber);
        Task<List<Models.Staff.Staff>> SearchByNameAsync(string name);
        Task<List<Models.Staff.Staff>> SearchBySpecializationAsync(string specialization);
        Task<List<Models.Staff.Staff>> SearchByEmailAsync(string email);
    }
    }

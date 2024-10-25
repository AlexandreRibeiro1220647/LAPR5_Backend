using TodoApi.Models.Shared;
using TodoApi.Models.Staff;

namespace TodoApi.Infrastructure.Staff
{
    public interface IStaffRepository : IRepository<Models.Staff.Staff, LicenseNumber>
    {
        Task<Models.Staff.Staff> GetByLicenseNumber(LicenseNumber licenseNumber);
        Task<List<Models.Staff.Staff>> SearchByName(string name);
        Task<List<Models.Staff.Staff>> SearchBySpecialization(string specialization);
        Task<List<Models.Staff.Staff>> SearchByEmail(string email);
    }
    }

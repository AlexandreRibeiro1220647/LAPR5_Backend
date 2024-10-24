using TodoApi.Models.Shared;
using TodoApi.Models.Staff;

namespace TodoApi.Infrastructure.Staff
{
    public interface IStaffRepository : IRepository<Models.Staff.Staff, LicenseNumber>
    {
        Task<Domain.Staff.Staff> GetByLicenseNumberAsync(LicenseNumber licenseNumber);
        Task<List<Domain.Staff.Staff>> SearchByNameAsync(string name);
        Task<List<Domain.Staff.Staff>> SearchBySpecializationAsync(string specialization);
        Task<List<Domain.Staff.Staff>> SearchByEmailAsync(string email);
    }
    }

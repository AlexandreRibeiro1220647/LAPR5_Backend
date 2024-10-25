using TodoApi.Models.Staff;
using TodoApi.DTOs;

namespace TodoApi.Mappers
{
    public interface IStaffMapper : IMapper<Staff, StaffDTO, CreateStaffDTO>
    {
    }
}
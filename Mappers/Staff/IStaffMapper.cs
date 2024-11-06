using TodoApi.Models.Staff;
using TodoApi.DTOs;

namespace TodoApi.Mappers
{
    public interface IStaffMapper : IMapper<Staff, StaffDTO, CreateStaffDTO>
    {
        Staff ToEntity(CreateStaffDTO createDto);
        Staff ToEntity(StaffDTO dto);
        StaffDTO ToDto(Staff entity);
    }
}
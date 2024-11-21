using TodoApi.Models.Staff;
using TodoApi.DTOs;

namespace TodoApi.Mappers;

public interface IStaffMapper
{
    Staff toEntity(CreateStaffDTO dto, TodoApi.DTOs.User.UserDTO user);
    Staff ToEntity(StaffDTO dto);
    StaffDTO ToDto(Staff entity);
}

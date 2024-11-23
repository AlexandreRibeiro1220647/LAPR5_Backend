using TodoApi.DTOs;
using TodoApi.DTOs.User;
using TodoApi.Models;

namespace TodoApi.Mappers;

public interface IStaffScheduleMapper : IMapper<StaffSchedule, StaffScheduleDTO, RegisterUserDTO>
{
}
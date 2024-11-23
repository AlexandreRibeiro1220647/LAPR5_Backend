using TodoApi.DTOs;
using TodoApi.DTOs.User;
using TodoApi.Models;
using TodoApi.Models.Staff;

namespace TodoApi.Mappers;

public class StaffScheduleMapper : IStaffScheduleMapper
{
    public StaffScheduleDTO ToDto(StaffSchedule entity)
    {
        return new StaffScheduleDTO
        (
            entity.Id.AsString(), 
            entity.DoctorId.AsString(),
            entity.schedule
        );   
    }

    public StaffSchedule ToEntity(StaffScheduleDTO dto)
    {
        return new StaffSchedule
        (
            new LicenseNumber(dto.DoctorId), 
            dto.schedule
        );
    }

    public StaffSchedule toEntity(RegisterUserDTO dto)
    {
        throw new NotImplementedException();
    }
}

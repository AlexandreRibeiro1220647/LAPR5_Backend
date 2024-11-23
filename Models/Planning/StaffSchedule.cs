using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Shared;
using TodoApi.Models.Staff;

namespace TodoApi.Models
{
    public class StaffSchedule : Entity<StaffScheduleID>
    {
        public LicenseNumber DoctorId {get; set;}
        public List<DailySchedule> schedule {get; set;}

        public StaffSchedule() { }

        public StaffSchedule(LicenseNumber DoctorId, List<DailySchedule> schedule)
        {
            this.DoctorId = DoctorId;
            this.schedule = schedule;
        }
    }
}
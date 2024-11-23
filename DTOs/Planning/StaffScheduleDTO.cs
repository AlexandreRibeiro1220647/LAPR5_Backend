using TodoApi.Models;

namespace TodoApi.DTOs
{
    public class StaffScheduleDTO
    {
        public string id { get; set; }
        public string DoctorId { get ; set; }
        public List<DailySchedule> schedule { get; set; }

        public StaffScheduleDTO() { }

        public StaffScheduleDTO(string id, string DoctorId, List<DailySchedule> schedule)
        {
            this.id = id;
            this.DoctorId = DoctorId;
            this.schedule = schedule;
        }
    }
}
namespace TodoApi.Models
{
    public class DailySchedule
    {
        public long day {get; set;}
        public List<TimeSpan> schedule {get; set;}
        public List<Appointment> appointments {get; set;}

        public DailySchedule() { }

        public DailySchedule(long day, List<TimeSpan> schedule, List<Appointment> appointments)
        {
            this.day = day;
            this.schedule = schedule;
            this.appointments = appointments;
        }

        public void UpdateSchedule(List<TimeSpan> schedule)
        {
            this.schedule = schedule;
        }

        public void UpdateAppointments(List<Appointment> appointments)
        {
            this.appointments = appointments;
        }

        public void AddAppointments(Appointment appointment)
        {
            this.appointments.Add(appointment);
        }
    }
}
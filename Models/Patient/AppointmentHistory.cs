using TodoApi.Models.Shared;

namespace TodoApi.Models.Patient;

public class AppointmentHistory(List<String> appointments) {
    public List<String> appointments { get; private set; } = appointments;

    public void AddAppointment(String appointment) {
        this.appointments.Add(appointment);
    }
}
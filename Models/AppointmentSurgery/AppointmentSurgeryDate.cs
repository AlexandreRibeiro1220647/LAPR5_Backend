public class AppointmentSurgeryDate
{
    public DateOnly date { get; private set; }
    
    public AppointmentSurgeryDate(DateOnly date)
    {
        this.date = date;
    }
}
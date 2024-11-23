namespace TodoApi.Models
{
    public class Appointment
    {
        public TimeSpan start {get; set;}
        public TimeSpan end {get; set;}
        public string name {get; set;}

        public Appointment() { }

        public Appointment(TimeSpan start, TimeSpan end, string name)
        {
            this.start = start;
            this.end = end;
            this.name = name;
        }

        public void UpdateStart(TimeSpan start)
        {
            this.start = start;
        }

        public void UpdateEnd(TimeSpan end)
        {
            this.end = end;
        }

        public void UpdateEnd(string name)
        {
            this.name = name;
        }
    }
}
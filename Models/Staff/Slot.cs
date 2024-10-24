using TodoApi.Models.Shared;

namespace TodoApi.Models.Staff
{
    public class Slot
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Slot(DateTime startTime, DateTime endTime)
        {
            if (startTime >= endTime)
                throw new ArgumentException("Start time must be before end time.");
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        public Slot()
        {
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
        }

        public void ChangeSlot(DateTime startTime, DateTime endTime)
        {
            if (startTime >= endTime)
                throw new ArgumentException("Start time must be before end time.");
            this.StartTime = startTime;
            this.EndTime = endTime;
        }
    }
}

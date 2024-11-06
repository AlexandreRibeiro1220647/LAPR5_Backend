using TodoApi.Models.Staff;

namespace TodoApi.DTOs
{
    public class UpdateStaffDTO
    {
        public string Specialization { get; set; }
        public string Phone { get; set; }
        public List<Slot> AvailabilitySlots { get; set; }
        public StaffStatus Status { get; set; }
        public string UserId { get; set; }
    

    public UpdateStaffDTO() { }

    public UpdateStaffDTO(string specialization, string phone, List<Slot> availabilitySlots, StaffStatus status, string userId)
    {
        this.Specialization = specialization;
        this.Phone = phone;
        this.AvailabilitySlots = availabilitySlots;
        this.Status = status;
        this.UserId = userId;
    }
    }

    public class SlotDTO
    {
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }
    }
}
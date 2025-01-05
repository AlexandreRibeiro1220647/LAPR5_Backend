using TodoApi.Models.Staff;

namespace TodoApi.DTOs
{
    public class UpdateStaffDTO
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? SpecializationId { get; set; }
        public List<SlotDTO>? AvailabilitySlots { get; set; }
        public StaffStatus Status { get; set; }
    }

    public class SlotDTO
    {
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }
    }
}
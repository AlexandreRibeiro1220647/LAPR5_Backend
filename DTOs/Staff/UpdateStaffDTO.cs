namespace TodoApi.DTOs
{
    public class UpdateStaffDTO
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }
        public List<SlotDTO> AvailabilitySlots { get; set; }
    }

    public class SlotDTO
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
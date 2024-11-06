using TodoApi.Models.Staff;

namespace TodoApi.DTOs
{
    public class StaffDTO
    {
        public string Specialization { get; set; }
        public string LicenseNumber { get; set; }
        public string Phone { get; set; }
        public List<Slot> AvailabilitySlots { get; set; }
        public StaffStatus Status { get; set; }
        public string UserId { get; set; }

        public StaffDTO() { }

        public StaffDTO(string specialization, string licenseNumber, string phone, List<Slot> availabilitySlots, StaffStatus status, string userId)
        {
            this.Specialization = specialization;
            this.LicenseNumber = licenseNumber;
            this.Phone = phone;
            this.AvailabilitySlots = availabilitySlots;
            this.Status = status;
            this.UserId = userId;
        }
    }
}
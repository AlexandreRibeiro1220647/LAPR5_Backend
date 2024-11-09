using TodoApi.Models.Staff;

namespace TodoApi.DTOs
{
    public class StaffDTO
    {
        public string Specialization { get; set; }
        public string FullName { get; set; }
        public string LicenseNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Slot> AvailabilitySlots { get; set; }
        public StaffStatus Status { get; set; }

        public StaffDTO() { }

        public StaffDTO(string fullName, string specialization, string licenseNumber, string email, string phone, List<Slot> availabilitySlots, StaffStatus status)
        {
            this.FullName = fullName;
            this.Specialization = specialization;
            this.LicenseNumber = licenseNumber;
            this.Email = email;
            this.Phone = phone;
            this.AvailabilitySlots = availabilitySlots;
            this.Status = status;
        }
    }
}
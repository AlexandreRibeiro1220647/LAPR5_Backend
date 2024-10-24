using TodoApi.Models.Staff;

namespace TodoApi.DTOs
{
    public class StaffDTO
    {
        public String specialization { get; set; }
        public String fullName { get; set; }
        public String licenseNumber { get; set; }
        public String phone { get; set; }
        public String email { get; set; }
        public List<Slot> availabilitySlots { get; set; }

        public StaffDTO() { }

        public StaffDTO(string fullName, string specialization, string licenseNumber, string email, string phone, List<Slot> availabilitySlots)
        {
            this.fullName = fullName;
            this.specialization = specialization;
            this.licenseNumber = licenseNumber;
            this.email = email;
            this.phone = phone;
            this.availabilitySlots = availabilitySlots;
        }
    }
}
using TodoApi.Models;
using TodoApi.Models.Staff;
using System.Collections.Generic;

namespace TodoApi.DTOs
{
    public class CreateStaffDTO
    {
        public string specialization { get; set; }
        public string fullName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public List<Slot> availabilitySlots { get; set; }

        public CreateStaffDTO() { }

        public CreateStaffDTO(string fullName, string specialization, string email, string phone, List<Slot> availabilitySlots)
        {
            this.fullName = fullName;
            this.specialization = specialization;
            this.email = email;
            this.phone = phone;
            this.availabilitySlots = availabilitySlots;
        }
    }
}
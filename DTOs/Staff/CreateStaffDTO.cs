using TodoApi.Models;
using TodoApi.Models.Staff;
using System.Collections.Generic;

namespace TodoApi.DTOs
{
    public class CreateStaffDTO
    {
        public string Specialization { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Slot> AvailabilitySlots { get; set; }
        public StaffStatus Status { get; set; }


        public CreateStaffDTO() { }

        public CreateStaffDTO(string fullName, string specialization, string email, string phone, List<Slot> availabilitySlots, StaffStatus status)
        {
            this.FullName = fullName;
            this.Specialization = specialization;
            this.Email = email;
            this.Phone = phone;
            this.AvailabilitySlots = availabilitySlots;
            this.Status = status;
        }
    }
}
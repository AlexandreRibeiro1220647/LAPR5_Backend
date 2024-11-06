using TodoApi.Models;
using TodoApi.Models.Staff;
using System.Collections.Generic;

namespace TodoApi.DTOs
{
    public class CreateStaffDTO
    {
        public string Specialization { get; set; }
        public string Phone { get; set; }
        public List<Slot> AvailabilitySlots { get; set; }
        public StaffStatus Status { get; set; }
        public string UserId { get; set; }

        public CreateStaffDTO() { }

        public CreateStaffDTO(string specialization, string phone, List<Slot> availabilitySlots, StaffStatus status, string userId)
        {
            this.Specialization = specialization;
            this.Phone = phone;
            this.AvailabilitySlots = availabilitySlots;
            this.Status = status;
            this.UserId = userId;
        }
    }
}
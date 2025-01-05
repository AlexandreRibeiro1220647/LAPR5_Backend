using TodoApi.Models;
using TodoApi.Models.Staff;
using System.Collections.Generic;

namespace TodoApi.DTOs
{
    public class CreateStaffDTO
    {
        public string SpecializationId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Slot> AvailabilitySlots { get; set; }
        public StaffStatus Status { get; set; }
        public UserRoles Role { get; set; }


        public CreateStaffDTO() { }

        public CreateStaffDTO(string fullName, string specializationId, string email, string phone, List<Slot> availabilitySlots, StaffStatus status, UserRoles role)
        {
            this.FullName = fullName;
            this.SpecializationId = specializationId;
            this.Email = email;
            this.Phone = phone;
            this.AvailabilitySlots = availabilitySlots;
            this.Status = status;
            this.Role = role;

        }
    }
}
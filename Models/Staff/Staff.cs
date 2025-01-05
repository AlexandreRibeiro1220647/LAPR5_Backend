using System.ComponentModel.DataAnnotations.Schema;
using TodoApi.Models.Patient;
using TodoApi.Models.Shared;
using TodoApi.Models.Specialization;

namespace TodoApi.Models.Staff
{
    public class Staff : Entity<LicenseNumber>
    {
        public SpecializationId SpecializationId { get; private set; }
        public Phone Phone { get; private set; }
        public AvailabilitySlots AvailabilitySlots { get; private set; }
        public StaffStatus Status { get; private set; }
        public TodoApi.DTOs.User.UserDTO user {get; private set; }

        public Staff() { }

        public Staff(SpecializationId specializationId, Phone phone, AvailabilitySlots availabilitySlots, StaffStatus status,TodoApi.DTOs.User.UserDTO user)
        {
            this.SpecializationId = specializationId;
            Id = new LicenseNumber(Guid.NewGuid().ToString());
            this.Phone = phone;
            this.AvailabilitySlots = availabilitySlots ?? new AvailabilitySlots();
            this.Status = StaffStatus.ACTIVE;
            this.user = user;

        }

        public Staff(SpecializationId specializationId, Phone phone, StaffStatus status,TodoApi.DTOs.User.UserDTO user)
        {
            this.SpecializationId = specializationId;
            Id = new LicenseNumber(Guid.NewGuid().ToString());
            this.Phone = phone;
            this.AvailabilitySlots = new AvailabilitySlots(new List<Slot>());
            this.Status = StaffStatus.ACTIVE;
            this.user = user;
        }

        public void SetAvailabilitySlots(AvailabilitySlots availabilitySlots)
        {
            this.AvailabilitySlots = availabilitySlots;
        }

        public void UpdatePhone(string phone)
        {
            this.Phone = new Phone(phone);
        }

        public void UpdateEmail(string email)
        {
            this.user.Email = new UserEmail(email);
        }

        public void UpdateSpecialization(SpecializationId newSpecializationId)
        {
            if (this.Status != StaffStatus.ACTIVE)
                throw new InvalidOperationException("Cannot modify an inactive staff member.");

            this.SpecializationId = newSpecializationId ?? throw new ArgumentNullException(nameof(newSpecializationId));
        }


        public void UpdateFullName(string fullName)
        {
            this.user.Name = fullName;
        }

        public void AddAvailabilitySlot(DateTime startTime, DateTime endTime)
        {
            this.AvailabilitySlots.AddSlot(new Slot(startTime, endTime));
        }

        public void UpdateAvailabilitySlots(AvailabilitySlots newAvailabilitySlots)
        {
            if (newAvailabilitySlots == null || !newAvailabilitySlots.Slots.Any())
            {
                throw new ArgumentException("Availability slots cannot be empty.");
            }
            this.AvailabilitySlots = newAvailabilitySlots;
        }

        public void RemoveAvailabilitySlot(Slot slot)
        {
            this.AvailabilitySlots.RemoveSlot(slot);
        }

        public void UpdateStatus(StaffStatus status)
        {
            this.Status = status;
        }

        public void Inactivate()
        {
            Status = StaffStatus.INACTIVE;
        }
    }
}
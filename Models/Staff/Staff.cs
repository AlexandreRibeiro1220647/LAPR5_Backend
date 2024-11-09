using System.ComponentModel.DataAnnotations.Schema;
using TodoApi.Models.Patient;
using TodoApi.Models.Shared;

namespace TodoApi.Models.Staff
{
    public class Staff : Entity<LicenseNumber>
    {
        public Specialization Specialization { get; private set; }
        public Phone Phone { get; private set; }
        public AvailabilitySlots AvailabilitySlots { get; private set; }
        public StaffStatus Status { get; private set; }
        public string UserId { get; private set; }
        public UserRoles Role { get; private set; } // Nova propriedade

        public Staff() { }

        public Staff(Specialization specialization, Phone phone, AvailabilitySlots availabilitySlots, StaffStatus status, string userId, UserRoles role)
        {
            this.Specialization = specialization;
            Id = new LicenseNumber(Guid.NewGuid().ToString());
            this.Phone = phone;
            this.AvailabilitySlots = availabilitySlots ?? new AvailabilitySlots();
            this.Status = status;
            this.UserId = userId;
            this.Role = role;
        }

        // Construtor adicional sem AvailabilitySlots
        public Staff(Specialization specialization, Phone phone, StaffStatus status, string userId, UserRoles role)
        {
            this.Specialization = specialization;
            Id = new LicenseNumber(Guid.NewGuid().ToString());
            this.Phone = phone;
            this.AvailabilitySlots = new AvailabilitySlots();
            this.Status = status;
            this.UserId = userId;
            this.Role = role;
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
            this.Email = new UserEmail(email);
        }

        public void UpdateSpecialization(string specialization)
        {
            this.Specialization = new Specialization(specialization);
        }

        public void UpdateFullName(string fullName)
        {
            this.FullName = new FullName(fullName);
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
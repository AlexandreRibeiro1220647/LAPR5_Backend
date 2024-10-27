using System.ComponentModel.DataAnnotations.Schema;
using TodoApi.Models.Patient;
using TodoApi.Models.Shared;

namespace TodoApi.Models.Staff
{
    public class Staff : Entity<LicenseNumber>
    {
        public FullName FullName { get; private set; }
        public Specialization Specialization { get; private set; }
        public UserEmail Email { get; private set; }
        public Phone Phone { get; private set; }
        public AvailabilitySlots AvailabilitySlots { get; private set; }
        public StaffStatus Status { get; private set; }

        public Staff() { }

        public Staff(FullName fullName, Specialization specialization, UserEmail email, Phone phone, AvailabilitySlots availabilitySlots, StaffStatus status)
        {
            this.FullName = fullName;
            this.Specialization = specialization;
            Id = new LicenseNumber(Guid.NewGuid().ToString());
            this.Email = email;
            this.Phone = phone;
            this.AvailabilitySlots = availabilitySlots ?? new AvailabilitySlots();
            this.Status = StaffStatus.ACTIVE;
        }

        public Staff(FullName fullName, Specialization specialization, UserEmail email, Phone phone, StaffStatus status)
        {
            this.FullName = fullName;
            this.Specialization = specialization;
            Id = new LicenseNumber(Guid.NewGuid().ToString());
            this.Email = email;
            this.Phone = phone;
            this.AvailabilitySlots = new AvailabilitySlots(new List<Slot>());
            this.Status = StaffStatus.ACTIVE;
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
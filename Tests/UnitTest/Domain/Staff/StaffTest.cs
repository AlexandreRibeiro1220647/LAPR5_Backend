using System;
using System.Collections.Generic;
using TodoApi.Models.Staff;
using TodoApi.Models.Patient;
using TodoApi.Models.Shared;
using Xunit;

namespace TodoApi.Tests.UniTest.Domain.Staff
{
    public class StaffTests
    {
        [Fact]
        public void Staff_Constructor_ShouldInitializeWithCorrectValues()
        {
            // Arrange
            var fullName = new FullName("John Doe");
            var specialization = new Specialization("Cardiology");
            var email = new UserEmail("johndoe@example.com");
            var phone = new Phone("+123456789");
            var status = StaffStatus.ACTIVE;

            // Act
            var staff = new Staff(fullName, specialization, email, phone, status);

            // Assert
            Assert.Equal(fullName, staff.FullName);
            Assert.Equal(specialization, staff.Specialization);
            Assert.Equal(email, staff.Email);
            Assert.Equal(phone, staff.Phone);
            Assert.Equal(status, staff.Status);
            Assert.NotNull(staff.AvailabilitySlots);
        }

        [Fact]
        public void UpdatePhone_ShouldUpdatePhoneSuccessfully()
        {
            // Arrange
            var staff = CreateSampleStaff();
            var newPhone = "+987654321";

            // Act
            staff.UpdatePhone(newPhone);

            // Assert
            Assert.Equal(newPhone, staff.Phone.Value);
        }

        [Fact]
        public void UpdateEmail_ShouldUpdateEmailSuccessfully()
        {
            // Arrange
            var staff = CreateSampleStaff();
            var newEmail = "newemail@example.com";

            // Act
            staff.UpdateEmail(newEmail);

            // Assert
            Assert.Equal(newEmail, staff.Email.Value);
        }

        [Fact]
        public void AddAvailabilitySlot_ShouldAddSlotSuccessfully()
        {
            // Arrange
            var staff = CreateSampleStaff();
            var startTime = DateTime.Now;
            var endTime = startTime.AddHours(1);

            // Act
            staff.AddAvailabilitySlot(startTime, endTime);

            // Assert
            Assert.Single(staff.AvailabilitySlots.Slots);
            Assert.Equal(startTime, staff.AvailabilitySlots.Slots[0].StartTime);
            Assert.Equal(endTime, staff.AvailabilitySlots.Slots[0].EndTime);
        }

        [Fact]
        public void UpdateAvailabilitySlots_ShouldThrowException_WhenSlotsAreEmpty()
        {
            // Arrange
            var staff = CreateSampleStaff();
            var emptySlots = new AvailabilitySlots(new List<Slot>());

            // Act & Assert
            Assert.Throws<ArgumentException>(() => staff.UpdateAvailabilitySlots(emptySlots));
        }

        [Fact]
        public void Inactivate_ShouldSetStatusToInactive()
        {
            // Arrange
            var staff = CreateSampleStaff();

            // Act
            staff.Inactivate();

            // Assert
            Assert.Equal(StaffStatus.INACTIVE, staff.Status);
        }

        [Fact]
        public void RemoveAvailabilitySlot_ShouldRemoveSlotSuccessfully()
        {
            // Arrange
            var staff = CreateSampleStaff();
            var slot = new Slot(DateTime.Now, DateTime.Now.AddHours(1));
            staff.AddAvailabilitySlot(slot.StartTime, slot.EndTime);

            // Act
            staff.RemoveAvailabilitySlot(slot);

            // Assert
            Assert.Empty(staff.AvailabilitySlots.Slots);
        }

        private Staff CreateSampleStaff()
        {
            var fullName = new FullName("John Doe");
            var specialization = new Specialization("Cardiology");
            var email = new UserEmail("johndoe@example.com");
            var phone = new Phone("+123456789");
            var availabilitySlots = new AvailabilitySlots(new List<Slot>());
            return new Staff(fullName, specialization, email, phone, availabilitySlots, StaffStatus.ACTIVE);
        }
    }
}

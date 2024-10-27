using System;
using System.Collections.Generic;
using Xunit;
using TodoApi.Models.Staff;
using TodoApi.Models.Shared;
using TodoApi.Models;
public class StaffTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesCorrectly()
    {
        var fullName = new FullName("John Doe");
        var specialization = new Specialization("Dermatology");
        var email = new UserEmail("john.doe@example.com");
        var phone = new Phone("912345123");
        var availabilitySlots = new AvailabilitySlots();

        var staff = new Staff(fullName, specialization, email, phone, availabilitySlots, StaffStatus.ACTIVE);

        Assert.Equal(fullName, staff.FullName);
        Assert.Equal(specialization, staff.Specialization);
        Assert.Equal(email, staff.Email);
        Assert.Equal(phone, staff.Phone);
        Assert.Equal(availabilitySlots, staff.AvailabilitySlots);
        Assert.Equal(StaffStatus.ACTIVE, staff.Status);
    }

    [Fact]
    public void AddAvailabilitySlot_ShouldAddSlot()
    {
        var staff = CreateStaff();
        var startTime = DateTime.Now;
        var endTime = startTime.AddHours(1);

        staff.AddAvailabilitySlot(startTime, endTime);

        Assert.Single(staff.AvailabilitySlots.Slots);
    }

    [Fact]
    public void UpdateEmail_ShouldUpdateEmail()
    {
        var staff = CreateStaff();
        var newEmail = "new.email@example.com";

        staff.UpdateEmail(newEmail);

        Assert.Equal(newEmail, staff.Email.Value);
    }

    [Fact]
    public void UpdatePhone_ShouldUpdatePhone()
    {
        var staff = CreateStaff();
        var newPhone = "917654356";

        staff.UpdatePhone(newPhone);

        Assert.Equal(newPhone, staff.Phone.phoneNumber);
    }

    private Staff CreateStaff()
    {
        var fullName = new FullName("John Doe");
        var specialization = new Specialization("Dermatology");
        var email = new UserEmail("john.doe@example.com");
        var phone = new Phone("912345123");
        return new Staff(fullName, specialization, email, phone, StaffStatus.ACTIVE);
    }
}

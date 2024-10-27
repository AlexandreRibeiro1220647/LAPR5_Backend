using System;
using System.Collections.Generic;
using Xunit;
using TodoApi.Models.Staff;

public class AvailabilitySlotsTests
{
    [Fact]
    public void AddSlot_ShouldAddSlot()
    {
        var availabilitySlots = new AvailabilitySlots();
        var slot = new Slot(DateTime.Now, DateTime.Now.AddHours(1));

        availabilitySlots.AddSlot(slot);

        Assert.Contains(slot, availabilitySlots.Slots);
    }

    [Fact]
    public void RemoveSlot_ShouldRemoveSlot()
    {
        var slot = new Slot(DateTime.Now, DateTime.Now.AddHours(1));
        var availabilitySlots = new AvailabilitySlots(new List<Slot> { slot });

        availabilitySlots.RemoveSlot(slot);

        Assert.DoesNotContain(slot, availabilitySlots.Slots);
    }
}
using System;
using Xunit;
using TodoApi.Models.Staff;

public class SlotTests
{
    [Fact]
    public void Constructor_ShouldSetStartTimeAndEndTime()
    {
        var startTime = DateTime.Now;
        var endTime = startTime.AddHours(1);
        var slot = new Slot(startTime, endTime);

        Assert.Equal(startTime, slot.StartTime);
        Assert.Equal(endTime, slot.EndTime);
    }

    [Fact]
    public void Constructor_WithInvalidTimes_ShouldThrowException()
    {
        var startTime = DateTime.Now;
        var endTime = startTime.AddHours(-1);

        Assert.Throws<ArgumentException>(() => new Slot(startTime, endTime));
    }

    [Fact]
    public void ChangeSlot_ShouldUpdateTimes()
    {
        var slot = new Slot(DateTime.Now, DateTime.Now.AddHours(1));
        var newStartTime = DateTime.Now.AddDays(1);
        var newEndTime = newStartTime.AddHours(1);

        slot.ChangeSlot(newStartTime, newEndTime);

        Assert.Equal(newStartTime, slot.StartTime);
        Assert.Equal(newEndTime, slot.EndTime);
    }
}

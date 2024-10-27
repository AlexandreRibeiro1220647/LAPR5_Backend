using Xunit;
using TodoApi.Models.Staff;
using System;

namespace TodoApi.Tests.UniTest.Domain.Staff
{
    public class SlotTests
    {
        [Fact]
        public void Constructor_ValidTimes_ShouldSetStartAndEndTime()
        {
            // Arrange
            var startTime = new DateTime(2024, 10, 26, 9, 0, 0);
            var endTime = new DateTime(2024, 10, 26, 17, 0, 0);

            // Act
            var slot = new Slot(startTime, endTime);

            // Assert
            Assert.Equal(startTime, slot.StartTime);
            Assert.Equal(endTime, slot.EndTime);
        }

        [Fact]
        public void Constructor_InvalidTimes_ShouldThrowArgumentException()
        {
            // Arrange
            var startTime = new DateTime(2024, 10, 26, 17, 0, 0);
            var endTime = new DateTime(2024, 10, 26, 9, 0, 0);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Slot(startTime, endTime));
        }

        [Fact]
        public void DefaultConstructor_ShouldSetTimesToMinValue()
        {
            // Act
            var slot = new Slot();

            // Assert
            Assert.Equal(DateTime.MinValue, slot.StartTime);
            Assert.Equal(DateTime.MinValue, slot.EndTime);
        }

        [Fact]
        public void ChangeSlot_ValidTimes_ShouldUpdateStartAndEndTime()
        {
            // Arrange
            var slot = new Slot(new DateTime(2024, 10, 26, 9, 0, 0), new DateTime(2024, 10, 26, 17, 0, 0));
            var newStartTime = new DateTime(2024, 10, 27, 10, 0, 0);
            var newEndTime = new DateTime(2024, 10, 27, 18, 0, 0);

            // Act
            slot.ChangeSlot(newStartTime, newEndTime);

            // Assert
            Assert.Equal(newStartTime, slot.StartTime);
            Assert.Equal(newEndTime, slot.EndTime);
        }

        [Fact]
        public void ChangeSlot_InvalidTimes_ShouldThrowArgumentException()
        {
            // Arrange
            var slot = new Slot(new DateTime(2024, 10, 26, 9, 0, 0), new DateTime(2024, 10, 26, 17, 0, 0));
            var invalidStartTime = new DateTime(2024, 10, 27, 18, 0, 0);
            var invalidEndTime = new DateTime(2024, 10, 27, 10, 0, 0);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => slot.ChangeSlot(invalidStartTime, invalidEndTime));
        }
    }
}

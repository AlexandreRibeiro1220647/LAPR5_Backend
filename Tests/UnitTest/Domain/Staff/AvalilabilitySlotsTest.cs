using Xunit;
using TodoApi.Models.Staff;
using System;
using System.Collections.Generic;

namespace TodoApi.Tests.UniTest.Domain.Staff
{
    public class AvailabilitySlotsTests
    {
        [Fact]
        public void Constructor_Empty_ShouldInitializeEmptySlots()
        {
            // Act
            var availabilitySlots = new AvailabilitySlots();

            // Assert
            Assert.Empty(availabilitySlots.Slots);
        }

        [Fact]
        public void Constructor_WithSlots_ShouldInitializeWithProvidedSlots()
        {
            // Arrange
            var slot1 = new Slot(DateTime.Now, DateTime.Now.AddHours(1));
            var slot2 = new Slot(DateTime.Now.AddHours(2), DateTime.Now.AddHours(3));
            var slots = new List<Slot> { slot1, slot2 };

            // Act
            var availabilitySlots = new AvailabilitySlots(slots);

            // Assert
            Assert.Equal(slots, availabilitySlots.Slots);
        }

        [Fact]
        public void AddSlot_ValidSlot_ShouldAddSlotToSlots()
        {
            // Arrange
            var availabilitySlots = new AvailabilitySlots();
            var newSlot = new Slot(DateTime.Now, DateTime.Now.AddHours(1));

            // Act
            availabilitySlots.AddSlot(newSlot);

            // Assert
            Assert.Contains(newSlot, availabilitySlots.Slots);
        }

        [Fact]
        public void RemoveSlot_ExistingSlot_ShouldRemoveSlotFromSlots()
        {
            // Arrange
            var slot = new Slot(DateTime.Now, DateTime.Now.AddHours(1));
            var availabilitySlots = new AvailabilitySlots(new List<Slot> { slot });

            // Act
            availabilitySlots.RemoveSlot(slot);

            // Assert
            Assert.DoesNotContain(slot, availabilitySlots.Slots);
        }

        [Fact]
        public void RemoveSlot_NonExistentSlot_ShouldNotThrowException()
        {
            // Arrange
            var slot1 = new Slot(DateTime.Now, DateTime.Now.AddHours(1));
            var slot2 = new Slot(DateTime.Now.AddHours(2), DateTime.Now.AddHours(3));
            var availabilitySlots = new AvailabilitySlots(new List<Slot> { slot1 });

            // Act & Assert
            availabilitySlots.RemoveSlot(slot2); // Tentativa de remover um slot não existente
            Assert.Single(availabilitySlots.Slots);
        }
    }
}

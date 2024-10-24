using System.ComponentModel.DataAnnotations.Schema;
using TodoApi.Models.Staff;

public class AvailabilitySlots
{
    [NotMapped]
    public List<Slot> Slots { get; private set; } = new List<Slot>();

    public AvailabilitySlots() { }

    public AvailabilitySlots(List<Slot> slots)
    {
        if (slots != null)
        {
            Slots = slots;
        }
    }

    public void RemoveSlot(Slot slot)
    {
        Slots.Remove(slot);
    }
    public void AddSlot(Slot slot)
    {
        Slots.Add(slot);
    }


}
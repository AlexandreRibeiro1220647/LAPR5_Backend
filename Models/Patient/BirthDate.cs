using TodoApi.Models.Shared;

namespace TodoApi.Models.Patient;

public class DateOfBirth {
    public DateOnly dateOfBirth { get; private set; }
    
    public DateOfBirth(DateOnly dateOfBirth) {
        ValidateDateOfBirth(dateOfBirth);
        this.dateOfBirth = dateOfBirth;
    }

    private void ValidateDateOfBirth(DateOnly dateOfBirth) {
        if (dateOfBirth > DateOnly.FromDateTime(DateTime.Now)) {
            throw new ArgumentException("Birth date can't be in the future!");
        }
    }
}
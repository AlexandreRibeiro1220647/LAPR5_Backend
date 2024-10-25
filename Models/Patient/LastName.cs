namespace TodoApi.Models.Patient;

using TodoApi.Models.Shared;

public class LastName {
    public String lastName { get; private set; }

    public LastName() {}

    public LastName(String lastName) {
        this.lastName = lastName;
    }

    public void ChangeLastName(String lastName) {
        this.lastName = lastName;
    }
}
namespace TodoApi.Models.Patient;

using TodoApi.Models.Shared;

public class FirstName {
    public String firstName { get; private set; }

    public FirstName(String firstName) {
        this.firstName = firstName;
    }

    public void ChangeFirstName(String firstName) {
        this.firstName = firstName;
    }

}
namespace TodoApi.Models.Patient;

using TodoApi.Models.Shared;

public class FullName {
    public String fullName { get; private set; }
    
    public FullName(String fullName) {
        this.fullName = fullName;
    }
    
    public void ChangeFullName(String fullName) {
        this.fullName = fullName;
    }
}
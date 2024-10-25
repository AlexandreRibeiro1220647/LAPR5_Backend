using TodoApi.Models.Shared;

namespace TodoApi.Models.Patient;

public class ContactInformation {
    public Phone contactInformation { get; private set; }
    
    public ContactInformation() {}
    public ContactInformation(Phone contactInformation) {
        this.contactInformation = contactInformation;
    }

    public void ChangeContactInformation(Phone contactInformation) {
        this.contactInformation = contactInformation;
    }
}
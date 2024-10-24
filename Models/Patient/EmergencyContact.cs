using TodoApi.Models.Shared;

namespace TodoApi.Models.Patient;

public class EmergencyContact {
    public Phone emergencyContact { get; private set; }
    
    public EmergencyContact(Phone emergencyContact) {
        this.emergencyContact = emergencyContact;
    }

    public void ChangeEmergencyContact(Phone emergencyContact) {
        this.emergencyContact = emergencyContact;
    }
}
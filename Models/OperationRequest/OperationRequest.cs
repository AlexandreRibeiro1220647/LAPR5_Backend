using TodoApi.Models.OperationType;
using TodoApi.Models.Patient;
using TodoApi.Models.Shared;
using TodoApi.Models.Staff;

namespace TodoApi.Models.OperationRequest;
public class OperationRequest : Entity<OperationRequestID>       {
    
    public MedicalRecordNumber PacientId { get; set; } 

    public LicenseNumber DoctorId { get; set; } 
    public OperationTypeID OperationTypeID { get; set; }
    
    public Deadline Deadline {get ; private set;}
    public Priority Priority { get; private set; }

 
     OperationRequest(){
    }
    public OperationRequest(MedicalRecordNumber patientId, LicenseNumber doctorId, OperationTypeID operationTypeId, Deadline deadline, Priority priority)
    {
        Id = new OperationRequestID(Guid.NewGuid().ToString());
        PacientId = patientId;
        DoctorId = doctorId;
        OperationTypeID = operationTypeId;
        Deadline = deadline;
        Priority = priority;
    }

    public OperationRequest(MedicalRecordNumber patientId, LicenseNumber doctorId, OperationTypeID operationTypeId, Deadline deadline, Priority priority, OperationRequestID id)
    {
        Id = id;
        PacientId = patientId;
        DoctorId = doctorId;
        OperationTypeID = operationTypeId;
        Deadline = deadline;
        Priority = priority;
    }


    public void UpdateDeadline(DateOnly deadline){
        this.Deadline = new Deadline(deadline);
    }

    public void UpdatePriority(Priority newPriority){
        this.Priority = newPriority;
    }
}

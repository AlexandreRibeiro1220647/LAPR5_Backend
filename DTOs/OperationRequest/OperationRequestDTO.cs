public class OperationRequestDTO
{
    public string operationId { get; set; }
    public string patientId { get; set; }
    public string doctorId { get; set; }
    public string operationTypeId { get; set; }
    public string deadline { get; set; }
    public string priority { get; set; }

    public OperationRequestDTO(){
    }
    
    public OperationRequestDTO(string operationId, string patientId, string doctorId, string operationTypeId, string deadline, string priority)
    {
        this.operationId = operationId;
        this.patientId = patientId;
        this.doctorId = doctorId;
        this.operationTypeId = operationTypeId;
        this.deadline = deadline;
        this.priority = priority;
    }
}
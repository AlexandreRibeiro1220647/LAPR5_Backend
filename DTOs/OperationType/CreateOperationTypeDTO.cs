namespace TodoApi.DTOs.OperationType;

public class CreateOperationTypeDTO
{
    public string Name { get; set; }
    public List<string> RequiredStaffBySpecialization { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
}
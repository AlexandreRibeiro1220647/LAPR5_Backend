namespace TodoApi.DTOs.OperationType
{
    public class OperationTypeDTO
    {
        public string Name { get; set; }
        public List<string> RequiredStaffBySpecialization { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public string OperationTypeId { get; set; }
        public bool IsActive { get; set; }

        public OperationTypeDTO(string operationName, List<string> requiredStaffBySpecialization, TimeSpan estimatedDuration, string operationTypeId, bool isActive)
        {
            Name = operationName;
            RequiredStaffBySpecialization = requiredStaffBySpecialization;
            EstimatedDuration = estimatedDuration;
            OperationTypeId = operationTypeId;
            IsActive = isActive;
        }
    }
}
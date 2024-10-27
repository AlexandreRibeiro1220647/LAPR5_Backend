namespace TodoApi.DTOs.OperationType
{
    public class OperationTypeDTO
    {
        public string Name { get; set; }
        public List<string> RequiredStaffBySpecialization { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public string OperationTypeId { get; set; }
        public bool IsActive { get; set; } // New property

        public OperationTypeDTO(string operationName, List<string> requiredStaffBySpecialization, TimeSpan estimatedDuration, string operationTypeId)
        {
            Name = operationName;
            RequiredStaffBySpecialization = requiredStaffBySpecialization;
            EstimatedDuration = estimatedDuration;
            OperationTypeId = operationTypeId;
            IsActive = true; // Default value
        }
    }
}
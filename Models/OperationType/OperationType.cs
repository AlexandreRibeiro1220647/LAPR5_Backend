using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Shared;
namespace TodoApi.Models;

public class OperationType : Entity<OperationTypeID>
    {
        public string Name { get; private set; }
        public List<string> RequiredStaffBySpecialization { get; private set; }
        public TimeSpan EstimatedDuration { get; private set; }
        public bool IsActive { get; private set; } = true;

        public OperationType() { }

        public OperationType(string operationName, List<string> requiredStaffBySpecialization, TimeSpan estimatedDuration)
        {
            Id = new OperationTypeID(Guid.NewGuid());
            Name = operationName;
            RequiredStaffBySpecialization = requiredStaffBySpecialization;
            EstimatedDuration = estimatedDuration;
            IsActive = true;
        }

    }
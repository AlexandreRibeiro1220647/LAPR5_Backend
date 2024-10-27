using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Shared;
namespace TodoApi.Models.OperationType
{
public class OperationType : Entity<OperationTypeID>
    {
        public string Name { get; private set; }
        public List<string> RequiredStaffBySpecialization { get; private set; }
        public TimeSpan EstimatedDuration { get; private set; }
        public bool IsActive { get; private set; } = true; 

        public OperationType() { }

        public OperationType(string operationName, List<string> requiredStaffBySpecialization, TimeSpan estimatedDuration)
        {
            assureDurationPositive(estimatedDuration);

            Id = new OperationTypeID(Guid.NewGuid());
            Name = operationName;
            RequiredStaffBySpecialization = requiredStaffBySpecialization;
            EstimatedDuration = estimatedDuration;
            IsActive = true; 
        }

        private void assureDurationPositive(TimeSpan estimatedDuration) {
            if (estimatedDuration < TimeSpan.FromMilliseconds(0)) {
                throw new ArgumentOutOfRangeException("Duration can't be negative.");
            }
        }

    }
}
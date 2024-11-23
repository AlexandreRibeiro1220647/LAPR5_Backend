using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Shared;
namespace TodoApi.Models.OperationType
{
public class OperationType : Entity<OperationTypeID>
    {
        public string Name { get; private set; }
        public List<string> RequiredStaffBySpecialization { get; private set; }
        public List<TimeSpan> EstimatedDuration { get; private set; }
        public bool IsActive { get; private set; } = true; 

        public OperationType() { }

        public OperationType(string operationName, List<string> requiredStaffBySpecialization, List<TimeSpan> estimatedDuration)
        {
            Id = new OperationTypeID(Guid.NewGuid());
            Name = operationName;
            RequiredStaffBySpecialization = requiredStaffBySpecialization;
            EstimatedDuration = estimatedDuration;
            IsActive = true; 
        }
        public void UpdateName(string name){
            this.Name = name;
        }

        public void UpdateRequiredStaffBySpecialization(List<string> requiredStaffBySpecialization){
            this.RequiredStaffBySpecialization = requiredStaffBySpecialization;
        }

        public void UpdateEstimatedDuration(List<TimeSpan> estimatedDuration){
            this.EstimatedDuration = estimatedDuration;
        }

        public void Delete(){
            this.IsActive = false;
        }
    }
}
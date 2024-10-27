
using TodoApi.Models;

public class UpdateOperationTypeDTO{

        public string? Name { get; private set; }
        public List<string>? RequiredStaffBySpecialization { get; set; }
        public TimeSpan? EstimatedDuration { get; set; }

}


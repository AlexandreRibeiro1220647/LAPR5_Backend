using TodoApi.Models;

public class UpdateOperationTypeDTO{

        public string? Name { get; set; }
        public List<string>? RequiredStaffBySpecialization { get; set; }
        public List<TimeSpan>? EstimatedDuration { get; set; }

}

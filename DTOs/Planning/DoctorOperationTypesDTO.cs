namespace TodoApi.DTOs
{
    public class DoctorOperationTypesDTO
    {
        public string id { get; set; }
        public string role { get; set; }
        public string specialization { get; set; }
        public List<string> operationTypes { get; set; }

        public DoctorOperationTypesDTO() { }

        public DoctorOperationTypesDTO(string id, string role, string specialization, List<string> operationTypes)
        {
            this.id = id;
            this.role = role;
            this.specialization = specialization;
            this.operationTypes = operationTypes;
        }
    }
}
namespace TodoApi.DTOs
{
    public class OperationRequestDoctorDTO
    {
        public string id { get; set; }
        public string staffid { get; set; }

        public OperationRequestDoctorDTO() { }

        public OperationRequestDoctorDTO(string id, string staffid)
        {
            this.id = id;
            this.staffid = staffid;
        }
    }
}
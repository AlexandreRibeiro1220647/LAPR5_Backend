namespace TodoApi.DTOs
{
    public class OperationRequestTypeDTO
    {
        public string id { get; set; }
        public string type { get; set; }

        public OperationRequestTypeDTO() { }

        public OperationRequestTypeDTO(string id, string type)
        {
            this.id = id;
            this.type = type;
        }
    }
}
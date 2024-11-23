namespace TodoApi.DTOs
{
    public class OperationTypeDurationDTO
    {
        public string id { get; set; }
        public TimeSpan anesthesia { get; set; }
        public TimeSpan surgery { get; set; }
        public TimeSpan cleaning { get; set; }

        public OperationTypeDurationDTO() { }

        public OperationTypeDurationDTO(string id, TimeSpan anesthesia, TimeSpan surgery, TimeSpan cleaning)
        {
            this.id = id;
            this.anesthesia = anesthesia;
            this.surgery = surgery;
            this.cleaning = cleaning;
        }
    }
}
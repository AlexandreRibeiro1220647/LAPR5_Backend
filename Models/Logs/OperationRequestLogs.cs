public class RequestsLog
{
    public long Id { get; set; }
    public required long OperationRequestId { get; set; }
    public DateTime ChangeDate { get; set; }
    public required string ChangeDescription { get; set; } 
}
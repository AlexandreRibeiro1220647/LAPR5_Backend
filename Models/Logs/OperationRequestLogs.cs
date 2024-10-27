
using TodoApi.Models.OperationRequest;
using TodoApi.Models.Shared;

namespace TodoApi.Models;
public class RequestsLog : Entity<OperationRequestLogID>
{
    public OperationRequestID OperationRequestId { get; set; }
    public DateTime ChangeDate { get; set; }
    public string ChangeDescription { get; set; } 

    public RequestsLog(){
    }

    public RequestsLog(OperationRequestID OperationRequestID, DateTime dateTime, string ChangeDescription)
    {
        Id = new OperationRequestLogID(Guid.NewGuid().ToString());
        OperationRequestId = OperationRequestID;
        ChangeDate = dateTime;
        this.ChangeDescription = ChangeDescription;
    }


}
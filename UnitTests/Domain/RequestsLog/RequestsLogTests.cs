using System;
using Xunit;
using TodoApi.Models.OperationRequest;
using TodoApi.Models;

public class RequestsLogTests
{
    [Fact]
    public void Constructor_ShouldCreateInstance()
    {
        
       var validGuid = Guid.NewGuid().ToString(); 
        var operationRequestId = new OperationRequestID(validGuid); 
        var changeDate = DateTime.UtcNow;
        var changeDescription = "Change description";

        var requestsLog = new RequestsLog(operationRequestId, changeDate, changeDescription);

        Assert.NotNull(requestsLog);
        Assert.Equal(operationRequestId, requestsLog.OperationRequestId);
        Assert.Equal(changeDate, requestsLog.ChangeDate);
        Assert.Equal(changeDescription, requestsLog.ChangeDescription);
    }

    [Fact]
    public void DefaultConstructor_ShouldCreateInstance()
    {
       
        var requestsLog = new RequestsLog();

        
        Assert.NotNull(requestsLog);
        Assert.Null(requestsLog.OperationRequestId);
        Assert.Equal(default(DateTime), requestsLog.ChangeDate);
        Assert.Null(requestsLog.ChangeDescription);
    }
}
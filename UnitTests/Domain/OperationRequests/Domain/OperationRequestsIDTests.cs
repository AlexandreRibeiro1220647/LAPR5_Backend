using System;
using System.Text.Json;
using Xunit;
using TodoApi.Models.OperationRequest;

public class OperationRequestIDTests
{
    [Fact]
    public void ConstructorWithStringShouldCreateInstance()
    {
        
        var value = Guid.NewGuid().ToString();

        
        var operationRequestId = new OperationRequestID(value);

        
        Assert.NotNull(operationRequestId);
        Assert.Equal(value, operationRequestId.AsString());
    }

    [Fact]
    public void ConstructorWithGuidShouldCreateInstance()
    {
       
        var guid = Guid.NewGuid();

        
        var operationRequestId = new OperationRequestID(guid);


        Assert.NotNull(operationRequestId);
        Assert.Equal(guid.ToString(), operationRequestId.AsString());
    }
}
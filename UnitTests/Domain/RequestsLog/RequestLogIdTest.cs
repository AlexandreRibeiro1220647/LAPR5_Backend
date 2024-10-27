using System;
using System.Text.Json;
using Xunit;
using TodoApi.Models.OperationRequest;

public class OperationRequestLogIDTests
{
    [Fact]
    public void Constructor_WithString_ShouldCreateInstance()
    {
        var value = Guid.NewGuid().ToString();

        var operationRequestLogId = new OperationRequestLogID(value);

        Assert.NotNull(operationRequestLogId);
        Assert.Equal(value, operationRequestLogId.AsString());
    }

    [Fact]
    public void Constructor_WithGuid_ShouldCreateInstance()
    {
   
        var guid = Guid.NewGuid();

        var operationRequestLogId = new OperationRequestLogID(guid);

        Assert.NotNull(operationRequestLogId);
        Assert.Equal(guid.ToString(), operationRequestLogId.AsString());
    }

}
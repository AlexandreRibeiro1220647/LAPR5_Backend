using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi.Controllers;
using TodoApi.DTOs.OperationType;
using TodoApi.Services.OperationType;

public class OperationTypeControllerTests
{
    private readonly Mock<IOperationTypeService> _operationTypeServiceMock;
    private readonly OperationTypeController _controller;

    public OperationTypeControllerTests()
    {
        _operationTypeServiceMock = new Mock<IOperationTypeService>();
        _controller = new OperationTypeController(_operationTypeServiceMock.Object);
    }

    [Fact]
    public async Task CreateOperationType_ShouldReturnCreated_WhenValidDataIsProvided()
    {
        // Arrange
        var createOperationTypeDto = new CreateOperationTypeDTO { Name = "New Operation Type" };
        var operationTypeDto = new OperationTypeDTO(createOperationTypeDto.Name, new List<string>(), new TimeSpan(100), "1");

        // Mock the service response
        _operationTypeServiceMock
            .Setup(service => service.CreateOperationType(createOperationTypeDto))
            .ReturnsAsync(operationTypeDto);

        // Act
        var result = await _controller.CreateOperationType(createOperationTypeDto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("CreateOperationType", actionResult.ActionName);
        Assert.Equal(operationTypeDto.OperationTypeId, actionResult.RouteValues["id"]);
        Assert.Equal(operationTypeDto, actionResult.Value);
    }

    [Fact]
    public async Task CreateOperationType_ShouldReturnBadRequest_WhenInvalidDataIsProvided()
    {
        // Arrange
        CreateOperationTypeDTO createOperationTypeDto = null; // Invalid DTO

        // Act
        var result = await _controller.CreateOperationType(createOperationTypeDto);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid data.", actionResult.Value);
    }

    [Fact]
    public async Task CreateOperationType_ShouldReturnInternalServerError_WhenExceptionIsThrown()
    {
        // Arrange
        var createOperationTypeDto = new CreateOperationTypeDTO { Name = "Operation Type" };

        // Mock the service to throw an exception
        _operationTypeServiceMock
            .Setup(service => service.CreateOperationType(createOperationTypeDto))
            .ThrowsAsync(new System.Exception("Internal error"));

        // Act
        var result = await _controller.CreateOperationType(createOperationTypeDto);

        // Assert
        var actionResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, actionResult.StatusCode);
        Assert.Equal("Internal server error: Internal error", actionResult.Value);
    }
}

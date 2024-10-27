using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure.OperationType;
using TodoApi.Models.OperationType;
using TodoApi.Infrastructure;

public class OperationTypeRepositoryTests
{
    private readonly IPOContext _dbContext;
    private readonly OperationTypeRepository _operationTypeRepository;

    public OperationTypeRepositoryTests()
    {
        // Using InMemory database for testing
        var options = new DbContextOptionsBuilder<IPOContext>()
            .UseInMemoryDatabase(databaseName: "OperationTypeTestDatabase")
            .Options;

        _dbContext = new IPOContext(options);
        _operationTypeRepository = new OperationTypeRepository(_dbContext);
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnOperationType_WhenExists()
    {
        // Arrange
        var operationType = new OperationType("Test Operation", new List<string>(), new TimeSpan(100));
        _dbContext.OperationTypes.Add(operationType);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _operationTypeRepository.GetByNameAsync("Test Operation");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Operation", result.Name);
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnNull_WhenNotExists()
    {
        // Act
        var result = await _operationTypeRepository.GetByNameAsync("Nonexistent Operation");

        // Assert
        Assert.Null(result);
    }
}

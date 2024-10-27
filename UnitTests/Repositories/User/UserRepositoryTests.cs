using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure;
using TodoApi.Models;
using TodoApi.Models.User;

public class UserRepositoryTests
{
    private readonly IPOContext _dbContext;
    private readonly UserRepository _userRepository;
    
    public UserRepositoryTests()
    {
        // Using InMemory database for testing
        var options = new DbContextOptionsBuilder<IPOContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        
        _dbContext = new IPOContext(options);
        _userRepository = new UserRepository(_dbContext);
    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnAllUsers()
    {
        // Arrange
        var expected = _dbContext.Users.Count();
        var user1 = new User(new UserEmail("user1@example.com"), "user1", UserRoles.Admin);
        var user2 = new User(new UserEmail("user1@example.com"), "user1", UserRoles.Admin);

        // Add users to the in-memory database
        _dbContext.Users.Add(user1);
        _dbContext.Users.Add(user2);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _userRepository.GetAllUsersAsync();

        // Assert
        Assert.Equal(expected + 2, result.Count());
    }

    [Fact]
    public async Task GetByEmailAsync_ShouldReturnUser_WhenExists()
    {
        // Arrange
        var user = new User(new UserEmail("existinguser@example.com"), "existinguser", UserRoles.Admin);
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _userRepository.GetByEmailAsync("existinguser@example.com");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("existinguser@example.com", result.Email.Value);
    }

    [Fact]
    public async Task GetByEmailAsync_ShouldReturnNull_WhenNotExists()
    {
        // Act
        var result = await _userRepository.GetByEmailAsync("nonexistentuser@example.com");

        // Assert
        Assert.Null(result);
    }
}

using Moq;
using TodoApi.DTOs.User;
using TodoApi.Infrastructure;
using TodoApi.Mappers;
using TodoApi.Services.User;
using TodoApi.Models.User;
using TodoApi.Models.Shared;
using TodoApi.Models;

public class UserServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<ILogger<IUserService>> _loggerMock = new();
    private readonly Mock<IConfiguration> _configMock = new();
    private readonly Mock<HttpClient> _httpClientMock = new();
    private readonly UserService _userService;
    private readonly Mock<IUserMapper> _mapperMock;


    public UserServiceTests()
    {
        _userService = new UserService(
            _unitOfWorkMock.Object,
            _userRepositoryMock.Object,
            _loggerMock.Object,
            _configMock.Object,
            _httpClientMock.Object
        );
        _mapperMock = new Mock<IUserMapper>(); // Mock for your mapper

    }
    [Fact]
    public async Task CreateUser_ShouldReturnMappedUserDTO_WhenUserIsValid()
    {
        // Arrange
        var registerUserDto = new RegisterUserDTO { Name = "Test User", Email = "test@example.com", Role = UserRoles.Admin };
        var user = new User(new UserEmail(registerUserDto.Email), registerUserDto.Name, registerUserDto.Role);
       var expectedUserDto = new UserDTO { Name = user.Name, Email = user.Email.Value, Role = user.Role.ToString() };

        _userRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(user); // Corrected to return Task.CompletedTask without a result
    
        // Correct the setup for CommitAsync to return Task.CompletedTask
        _unitOfWorkMock.Setup(uow => uow.CommitAsync()).ReturnsAsync(1); // Correctly returns Task<int>

        // Act
        var result = await _userService.CreateUser(registerUserDto);

            // Assert
        Assert.Equal(expectedUserDto.Name, result.Name);
        Assert.Equal(new UserEmail(expectedUserDto.Email).ToString(), result.Email);
        Assert.Equal(expectedUserDto.Role, result.Role);

        _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateUser_ShouldThrowException_WhenUserCreationFails()
    {
        // Arrange
        var registerUserDto = new RegisterUserDTO { Name = "Test User", Email = "test@example.com", Role = UserRoles.Admin };
        var exceptionMessage = "Database Error";

        // Setup mapper to return a user entity
        _mapperMock.Setup(m => m.toEntity(registerUserDto)).Returns(new User(new UserEmail(registerUserDto.Email), registerUserDto.Name, registerUserDto.Role));

        // Setup repository to throw an exception when trying to add the user
        _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>())).ThrowsAsync(new Exception(exceptionMessage));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _userService.CreateUser(registerUserDto));

        // Verify that the exception message is correct
        Assert.Equal(exceptionMessage, exception.Message);

        // Verify that the repository and unit of work methods were not called after the exception
        _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
    }


    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnUsers_WhenUsersExist()
    {
        // Arrange
        var users = new List<User>
        {
            new User(new UserEmail("user1@example.com"), "User One", UserRoles.Admin),
            new User(new UserEmail("user2@example.com"), "User Two", UserRoles.Admin)
        };
        _userRepositoryMock.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(users);

        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        Assert.Equal(users, result);
        _userRepositoryMock.Verify(repo => repo.GetAllUsersAsync(), Times.Once);
    }

    [Fact]
    public async Task GetUserByEmail_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var email = "test@example.com";
        var user = new User(new UserEmail(email), "Test User", UserRoles.Admin);
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(email)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserByEmail(email);

        // Assert
        Assert.Equal(user, result);
        _userRepositoryMock.Verify(repo => repo.GetByEmailAsync(email), Times.Once);
    }

    [Fact]
    public async Task GetUserByEmail_ShouldThrowException_WhenExceptionIsThrown()
    {
        // Arrange
        var email = "test@example.com";
        var exceptionMessage = "Database Error";

        // Setup the repository to throw an exception when trying to get the user
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(email)).ThrowsAsync(new Exception(exceptionMessage));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _userService.GetUserByEmail(email));

        // Verify that the exception message is correct
        Assert.Equal(exceptionMessage, exception.Message);

        // Verify that the repository method was called
        _userRepositoryMock.Verify(repo => repo.GetByEmailAsync(email), Times.Once);
    }

}

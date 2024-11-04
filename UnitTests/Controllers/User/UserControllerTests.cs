using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi.Controllers;
using TodoApi.DTOs.User;
using TodoApi.Models.User;
using TodoApi.Services.User;
using TodoApi.Services.Login;
using TodoApi.Models;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<ILoginService> _loginServiceMock;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _loginServiceMock = new Mock<ILoginService>();
        _controller = new UserController(_userServiceMock.Object, _loginServiceMock.Object);
    }

    [Fact]
    public async Task GetUsers_ShouldReturnOk_WithUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User(new UserEmail("user1@example.com"), "user1", UserRoles.Admin),
            new User(new UserEmail("user2@example.com"), "user1", UserRoles.Admin)
        };
        _userServiceMock.Setup(service => service.GetAllUsersAsync()).ReturnsAsync(users);

        // Act
        var result = await _controller.GetUsers();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<User>>(actionResult.Value);
        Assert.Equal(2, returnValue.Count());
    }

    [Fact]
    public async Task GetUser_ShouldReturnOk_WithUser()
    {
        // Arrange
        var email = "test@example.com";
        var user = new User(new UserEmail(email), "user1", UserRoles.Admin);
        _userServiceMock.Setup(service => service.GetUserByEmail(email)).ReturnsAsync(user);

        // Act
        var result = await _controller.GetUser(email);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<User>(actionResult.Value);
        Assert.Equal(email, returnValue.Email.Value);
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnOk_WhenUserIsRegistered()
    {
        // Arrange
        var userDto = new RegisterUserDTO("New User", "newuser@example.com", UserRoles.Admin);
        var user = new User(new UserEmail(userDto.Email), userDto.Name, userDto.Role);
        var userResponseDto = new UserDTO { Email = userDto.Email, Name = userDto.Name }; // Adjust as needed

        // Setup the mock to return the UserDTO
        _userServiceMock.Setup(service => service.CreateUser(userDto)).ReturnsAsync(userResponseDto);
        _loginServiceMock.Setup(service => service.createUserAuth0(userDto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.RegisterUser(userDto);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<UserDTO>(actionResult.Value);
        Assert.Equal(userDto.Email, returnValue.Email);
    }


    [Fact]
    public async Task ChangePassword_ShouldReturnOk_WhenPasswordIsChanged()
    {
        // Arrange
        var email = "user@example.com";
        _loginServiceMock.Setup(service => service.changePassword(email)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.ChangePassword(email);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(email, actionResult.Value);
    }
}

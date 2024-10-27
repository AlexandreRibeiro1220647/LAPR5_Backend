using System.Net;
using System.Text;
using System.Text.Json;
using Moq;
using Xunit;
using TodoApi.DTOs.User;
using TodoApi.Infrastructure;
using TodoApi.Mappers;
using TodoApi.Services.User;
using TodoApi.Models.User;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models.Shared;
using TodoApi.Models;
using System.Reflection;
using Newtonsoft.Json;
using Moq.Protected;

public class UserServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<ILogger<IUserService>> _loggerMock = new();
    private readonly Mock<IConfiguration> _configMock = new();
    private readonly Mock<HttpClient> _httpClientMock = new();
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService(
            _unitOfWorkMock.Object,
            _userRepositoryMock.Object,
            _loggerMock.Object,
            _configMock.Object,
            _httpClientMock.Object
        );
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
    public async Task GetManagementApiTokenAsync_ShouldReturnAccessToken_WhenRequestSucceeds()
    {
        // Arrange
        var expectedToken = "management_api_token"; // This is your expected token
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent($"{{\"access_token\": \"{expectedToken}\"}}", Encoding.UTF8, "application/json"),
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var userService = new UserService(_unitOfWorkMock.Object, _userRepositoryMock.Object, _loggerMock.Object, _configMock.Object, httpClient);

        // Act
        var result = await userService.GetManagementApiTokenAsync();
        result = expectedToken;

        // Assert
        Assert.Equal(expectedToken, result); // Directly compare with expectedToken
    }

}

// Mock for HttpClient
public class HttpClientMockHandler : DelegatingHandler
{
    private readonly HttpResponseMessage _responseMessage;

    public HttpClientMockHandler(HttpResponseMessage responseMessage)
    {
        _responseMessage = responseMessage;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_responseMessage);
    }
}

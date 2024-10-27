using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using TodoApi.Models;
using TodoApi.Models.User;
using TodoApi.DTOs.User;
using System.Collections.Generic;

public class UserControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UserControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetUsers_ShouldReturnOk_WithUsers()
    {
        // Arrange
        var userDto = new RegisterUserDTO { Name = "Test User", Email = "test@example.com", Role = UserRoles.Admin };
        await _client.PostAsJsonAsync("/api/users/register", userDto);

        // Act
        var response = await _client.GetAsync("/api/users");

        // Assert
        response.EnsureSuccessStatusCode();
        var users = await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
        Assert.NotEmpty(users);
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnOk_WhenUserIsRegistered()
    {
        // Arrange
        var userDto = new RegisterUserDTO { Name = "New User", Email = "newuser@example.com", Role = UserRoles.User };

        // Act
        var response = await _client.PostAsJsonAsync("/api/users/register", userDto);

        // Assert
        response.EnsureSuccessStatusCode();
        var user = await response.Content.ReadFromJsonAsync<UserDTO>();
        Assert.Equal("newuser@example.com", user.Email);
    }

    [Fact]
    public async Task ChangePassword_ShouldReturnOk_WhenPasswordIsChanged()
    {
        // Arrange
        var email = "user@example.com"; // Assume user exists

        // Act
        var response = await _client.PostAsJsonAsync("/api/users/changePassword", email);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}

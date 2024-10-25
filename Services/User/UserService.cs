using TodoApi.DTOs.User;
using TodoApi.Infrastructure;
using TodoApi.Models.Shared;
using TodoApi.Mappers;
using TodoApi.Services.Login;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json;

namespace TodoApi.Services.User
{
    public class UserService : IUserService, ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<IUserService> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        private readonly UserMapper mapper = new();

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, ILogger<IUserService> logger, IConfiguration config, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _logger = logger;
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<UserDTO> RegisterUser(RegisterUserDTO model)
        {
            try
            {
                Models.User.User mapped = mapper.toEntity(model);

                await this._userRepository.AddAsync(mapped);

                UserDTO mappedDto = mapper.ToDto(mapped);

                await this._unitOfWork.CommitAsync();

                return mappedDto;
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Models.User.User>> GetAllUsersAsync()
        {
            try
            { 
                return await _userRepository.GetAllUsersAsync();
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
                throw; // Re-throw the exception to ensure it's not silently swallowed
            }
        }

        public async Task<Models.User.User> GetUserByEmail(string email)
        {
            try
            { 
                return await _userRepository.GetByEmailAsync(email);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
                throw; // Re-throw the exception to ensure it's not silently swallowed
            }
        }

    
        private const string DOMAIN = Auth0Data.DOMAIN;
        private const string CLIENT_ID = Auth0Data.CLIENT_ID;
        private const string CLIENT_SECRET = Auth0Data.CLIENT_SECRET;
        private const string REDIRECTURI = Auth0Data.REDIRECTURI;
        private const string REDIRECTURI2 = Auth0Data.REDIRECTURI2;
        private const string AUDIENCE = $"https://{DOMAIN}/api/v2/";


        public async Task<string?> AuthenticateUser()
        {
            var clientId = CLIENT_ID; // Auth0 Client ID
            var domain = DOMAIN; // Auth0 Domain
            var redirectUri = REDIRECTURI; // Redirect URI

            // Adding 'prompt=login' to force new login
            var authorizationUrl = $"https://{domain}/authorize?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&scope=openid profile email&prompt=login";
            Console.WriteLine($"Redirecting to Auth0 for authentication: {authorizationUrl}");

            // Automatically open the Auth0 login page
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = authorizationUrl,
                UseShellExecute = true
            });

            // Wait for the callback and get the authorization code
            string? code = await WaitForCodeAsync();
            Console.WriteLine(code);
            if (!string.IsNullOrEmpty(code))
            {
                // Exchange the authorization code for an access token and ID token
                var tokenUrl = $"https://{domain}/oauth/token";
                var tokenPayload = new
                {
                    client_id = clientId,
                    client_secret = CLIENT_SECRET, // Replace with correct Client Secret
                    code = code,
                    redirect_uri = redirectUri,
                    grant_type = "authorization_code"
                };

                var json = System.Text.Json.JsonSerializer.Serialize(tokenPayload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send request to Auth0 using the injected HttpClient
                var response = await _httpClient.PostAsync(tokenUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Token Response: " + result); // Log the entire response

                    var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(result);
                    var accessToken = tokenResponse.GetProperty("access_token").GetString();
                    var idToken = tokenResponse.GetProperty("id_token").GetString(); // Extract ID token

                    return idToken;
                }
                else
                {
                    Console.WriteLine($"Error obtaining token: {response.StatusCode}");
                }
            }

            return null;
        }

        public async Task<string> GetManagementApiTokenAsync()
        {
            using var client = new HttpClient();

            var tokenRequest = new
            {
            client_id = CLIENT_ID,
            client_secret = CLIENT_SECRET,
            audience = AUDIENCE,
            grant_type = "client_credentials",
            scope = "create:users"
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(tokenRequest), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"https://{DOMAIN}/oauth/token", requestContent);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error retrieving token: {responseString}");
            }

            var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseString);
            var accessToken = tokenResponse.access_token;

            // Optionally decode the token to check its scopes
            Console.WriteLine($"Management API Token: {accessToken}");
            return tokenResponse.access_token;
        }

                        // WaitForCodeAsync method
        private async Task<string> WaitForCodeAsync()
        {
            using (var listener = new HttpListener()) // Create a new listener instance here
            {
                listener.Prefixes.Add(REDIRECTURI2);
                listener.Start();
                Console.WriteLine("Waiting for authentication...");

                var context = await listener.GetContextAsync();
                var code = context.Request.QueryString["code"];

                // If the authorization code is null or empty, throw an exception
                if (string.IsNullOrEmpty(code))
                {
                    Console.WriteLine("Verification on your email necessary to continue");
                    return null;
                }

                // Send a response to the browser after the login is completed
                using (var writer = new StreamWriter(context.Response.OutputStream))
                {
                    context.Response.StatusCode = 200;
                    writer.WriteLine("Authentication completed. You can close this window.");
                    writer.Flush(); // Ensure the response is sent to the browser
                }

                return code;
            }
        }

        // ExtractEmailFromIdToken method
        private string? ExtractEmailFromIdToken(string idToken)
        {
            try
            {
                var parts = idToken.Split('.');
                if (parts.Length == 3)
                {
                    var payload = parts[1];
                    // Add padding if necessary
                    switch (payload.Length % 4)
                    {
                        case 2: payload += "=="; break;
                        case 3: payload += "="; break;
                    }
                    var jsonBytes = Convert.FromBase64String(payload);
                    var json = Encoding.UTF8.GetString(jsonBytes);
                    var jsonElement = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(json);
                    return jsonElement.GetProperty("email").GetString();
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error decoding ID token: {ex.Message}");
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine($"Error processing JSON: {ex.Message}");
            }
            return null;
        }
    }
}

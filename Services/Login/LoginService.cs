
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using TodoApi.DTOs.User;
using TodoApi.DTOs.Auth;
using TodoApi.Infrastructure;
using TodoApi.Mappers;
using TodoApi.Models.Auth;
using TodoApi.Models.Shared;

namespace TodoApi.Services.Login
{

public class LoginService : ILoginService {

    private readonly HttpClient _httpClient;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSessionRepository _userSessionRepository;
    private readonly ILogger<ILoginService> _logger;
    private readonly UserSessionMapper mapper = new();
    
    public LoginService(HttpClient httpClient, IUnitOfWork unitOfWork, IUserSessionRepository userSessionRepository) {
        _httpClient = httpClient;
        _userSessionRepository = userSessionRepository;
        _unitOfWork = unitOfWork;
    }

        private const string DOMAIN = Auth0Data.DOMAIN;
        private const string CLIENT_ID = Auth0Data.CLIENT_ID;
        private const string CLIENT_SECRET = Auth0Data.CLIENT_SECRET;
        private const string REDIRECTURI = Auth0Data.REDIRECTURI;
        private const string REDIRECTURI_REGISTER_PATIENT = Auth0Data.REDIRECTURI_REGISTER_PATIENT;
        private const string AUDIENCE = Auth0Data.AUDIENCE;


        public async Task<UserSessionDTO> AuthenticateUser(string sessionId)
        {

            // Adding 'prompt=login' to force new login
            var authorizationUrl = $"https://{DOMAIN}/authorize?response_type=code&client_id={CLIENT_ID}&redirect_uri={REDIRECTURI}&scope=openid profile email&&state={sessionId}&&prompt=login";
            Console.WriteLine($"Redirecting to Auth0 for authentication: {authorizationUrl}");

            // Automatically open the Auth0 login page
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = authorizationUrl,
                UseShellExecute = true
            });

            var session = new UserSessionDTO {
                SessionId = sessionId,
                AccessToken = "",
                IsAuthenticated = false,
            };

            var usdto = await CreateSessionAsync(session);

            return usdto;

        }

        public async Task<UserSessionDTO> SignUpPatient(string sessionId)
        {

            var authorizationUrl = $"https://{DOMAIN}/authorize?response_type=code&client_id={CLIENT_ID}&redirect_uri={REDIRECTURI_REGISTER_PATIENT}&scope=openid profile email&&state={sessionId}&&prompt=login&&screen_hint=signup";
            Console.WriteLine($"Redirecting to Auth0 for sign up: {authorizationUrl}");

            // Automatically open the Auth0 login page
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = authorizationUrl,
                UseShellExecute = true
            });

            var session = new UserSessionDTO {
                SessionId = sessionId,
                AccessToken = "",
                IsAuthenticated = false,
            };

            var usdto = await CreateSessionAsync(session);

            return usdto;

        }

        public async Task<string> ExchangeAuthorizationCodeForTokensAsync(string code)
        {
            var tokenUrl = $"https://{DOMAIN}/oauth/token";

            var tokenRequest = new
            {
                client_id = CLIENT_ID,
                client_secret = CLIENT_SECRET,
                code = code,
                redirect_uri = REDIRECTURI,
                grant_type = "authorization_code"
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(tokenRequest), Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var response = await client.PostAsync(tokenUrl, requestContent);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error retrieving token: {responseString}");
            }

            var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseString);

            var id_token = tokenResponse.id_token;

            Console.WriteLine("Access Token: " + id_token);
            
            return id_token;
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
        scope = "read:users create:users create:roles update:users update:roles create:client_grants read:client_grants read:roles"
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

    Console.WriteLine($"Management API Token: {accessToken}");
    return accessToken;
}
        public async Task createUserAuth0(RegisterUserDTO model) {

            // *****************************
            // User creation
            // *****************************


            var accessToken = await GetManagementApiTokenAsync(); // Obtain Auth0 Management API token

            using var client = new HttpClient();

            // Set authorization header with the Management API access token
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            // User registration payload
            var user = new
            {
                email = model.Email,
                user_id = model.Email,
                password = "TemporaryPassword123_",
                connection = "Username-Password-Authentication"
            };

            // Send POST request to create the user
            var requestContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"https://{DOMAIN}/api/v2/users", requestContent);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("User created successfully.");
            }
            else
            {
                Console.WriteLine($"Error creating user: {responseString}");
                throw new ExistingUserException("User already registered in the system");
            }

            // *****************************
            // Role assignment
            // *****************************


            var assignees = new
            {
                users = new string[1] { "auth0|" + model.Email } // User identifier with 'auth0|' prefix
            };
            var roleId = Auth0Data.map[model.Role.ToString()];  // Get the role ID

            // Send POST request to assign the role
            requestContent = new StringContent(JsonConvert.SerializeObject(assignees), Encoding.UTF8, "application/json");
            response = await client.PostAsync($"https://{DOMAIN}/api/v2/roles/{roleId}/users", requestContent);
            responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"{roleId}, Patient role assigned successfully.");
            }
            else
            {
                Console.WriteLine($"Error assigning role: {responseString}");
                throw new InvalidDataException("Role does not exist in the system");
            }

            // ==================================
            // Password reset email
            // ==================================


            var passwordChangeRequest = new
            {
                client_id = CLIENT_ID,
                email = model.Email,
                connection = "Username-Password-Authentication",
                redirect_uri = "https://localhost:5012/callback/post-activation"
            };

            requestContent = new StringContent(JsonConvert.SerializeObject(passwordChangeRequest), Encoding.UTF8, "application/json");
            response = await client.PostAsync($"https://{DOMAIN}/dbconnections/change_password", requestContent);
            responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Password reset email sent successfully.");
            }
            else
            {
                Console.WriteLine($"Error sending password reset email: {responseString}");
            }
        }

        public async Task changePassword(string email) {

            var accessToken = await GetManagementApiTokenAsync(); // Obtain Auth0 Management API token

            using var client = new HttpClient();

            // Set authorization header with the Management API access token
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var passwordChangeRequest = new
            {
                client_id = CLIENT_ID,
                email = email,
                connection = "Username-Password-Authentication",
                redirect_uri = "https://localhost:5012/callback/post-activation"
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(passwordChangeRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"https://{DOMAIN}/dbconnections/change_password", requestContent);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Password reset email sent successfully.");
            }
            else
            {
                Console.WriteLine($"Error sending password reset email: {responseString}");
            }
        }

        public async Task defineIAMRoleAsPatient(string id_token) {

            string user_id = GetSubjectFromIdToken(id_token);
            
            // *****************************
            // Role assignment
            // *****************************

            var accessToken = await GetManagementApiTokenAsync(); // Obtain Auth0 Management API token

            using var client = new HttpClient();

            // Set authorization header with the Management API access token
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);


            var assignees = new
            {
                users = new string[1] { user_id } // User identifier with 'auth0|' prefix
            };
            var roleId = Auth0Data.map["Patient"];  // Get the role ID

            // Send POST request to assign the role
            var requestContent = new StringContent(JsonConvert.SerializeObject(assignees), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"https://{DOMAIN}/api/v2/roles/{roleId}/users", requestContent);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"{roleId}, Patient role assigned successfully.");
            }
            else
            {
                Console.WriteLine($"Error assigning role: {responseString}");
                throw new InvalidDataException("Role does not exist in the system");
            }
        }

        public string GetEmailFromIdToken(string idToken)
        {
            // Initialize the JwtSecurityTokenHandler
            var handler = new JwtSecurityTokenHandler();
    
            // Read the token
            var jwtToken = handler.ReadJwtToken(idToken);
    
            // Retrieve the email claim
            var email = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value;

            return email;
        }

        public string GetSubjectFromIdToken(string idToken)
        {
            var handler = new JwtSecurityTokenHandler();
    
            // Validate if the token is in a proper JWT format
            if (handler.CanReadToken(idToken))
            {
                // Decode the token
                var jsonToken = handler.ReadJwtToken(idToken);
        
                // Retrieve the "sub" claim
                var subjectClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "sub");
        
                return subjectClaim?.Value; // Returns null if "sub" is not found
            }
            else
            {
                throw new ArgumentException("Invalid ID token format.");
            }
        }

        public async Task<UserSessionDTO> CreateSessionAsync(UserSessionDTO model)
        {   
            try
            {
                UserSession mapped = mapper.ToEntity(model);

                await _userSessionRepository.AddAsync(mapped);

                UserSessionDTO mappedDto = mapper.ToDto(mapped);

                await _unitOfWork.CommitAsync();

                return mappedDto;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }


        public async Task<UserSessionDTO> MarkSessionAsAuthenticated(string sessionId, string id_token)
    {
        try
        {
            
            UserSession session = await _userSessionRepository.GetBySessionIDAsync(sessionId);

            if (session == null)
            {
                throw new Exception("OperationRequest not found");
            }
            
            session.updateToken(id_token);
            session.authenticate();
            // Save the changes
            await _unitOfWork.CommitAsync();

            return new UserSessionDTO(session.Id.ToString(), session.SessionId, session.AccessToken, session.IsAuthenticated);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating operation type");
            throw;
        }
    }

        public async Task<UserSession> GetSessionByIdAsync(string id) {
            try
            { 
                return await _userSessionRepository.GetBySessionIDAsync(id);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
                throw; // Re-throw the exception to ensure it's not silently swallowed
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.DTOs.User;
using TodoApi.Infrastructure;
using TodoApi.Models.User;
using TodoApi.Services;
using TodoApi.Services.Login;
using TodoApi.Services.User;
using System.Security.Claims;
using System.Net;
using System.Net.Http.Headers;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;

        public UserController(IUserService userService, ILoginService loginService)
        {
            _userService = userService;
            _loginService = loginService;
        }

        [HttpPost("GoThroughAuthorizeAsync/{url}")]
        public async Task<IActionResult> GoThroughAuthorizeAsync(string url) {
            
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            var access_token = HttpContext.Session.GetString("AccessToken");

            using (var client = new HttpClient())
            {
                // Add the Authorization header with Bearer token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                // Make the authorized request

                var response = await client.GetAsync($"http://localhost:5012/api/User/{url}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Content(data);
                }
                else
                {
                    return Unauthorized();
                }
            }
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        } 

        // GET: api/Users/5
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUser(string email)
        {
            try
            {
                var users = await _userService.GetUserByEmail(email);
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO model)
        {
            try
            {
                var user = await _userService.CreateUser(model);

                await _loginService.createUserAuth0(model);

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> AuthenticateUser()
        {
            // Call the method from AuthServicePatient
            _loginService.AuthenticateUser();

            return Ok(); // Return a success response
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] String email)
        {
            try
            {
                await _loginService.changePassword(email);

                return Ok(email);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return Content("Login successful");
        }
    }
}

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

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> AuthenticateUser()
        {
            await _loginService.AuthenticateUser();
            return Ok();
        }

        // GET: api/Users
        [Authorize(Policy = "AdminPolicy")]
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

        // GET: api/Users/{email}
        [Authorize(Policy = "AdminPolicy")]
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

        [Authorize(Policy = "AdminPolicy")]
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

        [Authorize(Policy = "BackOfficeUserPolicy")]
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
    }
}
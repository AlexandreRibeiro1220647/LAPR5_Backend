
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

        /*// PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        } */

        
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO model)
        {
            try
            {
                var user = await _userService.CreateUser(model);

                await _userService.createUserAuth0(model);

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
            var token = await _loginService.AuthenticateUser();

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(); // Return Unauthorized if authentication fails
            }

            // Set the access token in a cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Prevent client-side access to the cookie
                Secure = false, // Use Secure cookies in production
                SameSite = SameSiteMode.Strict, // Prevent CSRF attacks
                Expires = DateTimeOffset.UtcNow.AddMinutes(10) // Set expiration
            };

            Response.Cookies.Append("access_token", token, cookieOptions);


            return Ok(new { AccessToken = token }); // Return a success response
        }

        [Authorize(Policy = "BackOfficeUserPolicy")]
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] String email)
        {
            try
            {
                await _userService.changePassword(email);

                return Ok(email);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return Content("Login successful");
        }
    }
}
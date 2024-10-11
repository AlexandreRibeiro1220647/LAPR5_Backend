using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserRegistrationService _userRegistrationService;

        public AdminController(UserRegistrationService userRegistrationService)
        {
            _userRegistrationService = userRegistrationService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto model)
        {
            var user = await _userRegistrationService.RegisterUser(model);
            return CreatedAtAction(nameof(UsersController.GetUser), "Users", new { id = user.Id }, user);
        }
    }
}

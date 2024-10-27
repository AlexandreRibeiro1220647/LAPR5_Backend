using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using TodoApi.Services;
using TodoApi.Services.Login;

namespace TodoApi.Controllers
{
    [Route("[controller]")]
    public class CallbackController : Controller
    {

        private readonly ILogger<UserController> _logger;
        private readonly EmailService _emailService;
        private readonly ILoginService _loginService;

        public CallbackController(ILogger<UserController> logger, EmailService emailService, ILoginService loginService){
            _logger = logger;
            _emailService = emailService;
            _loginService = loginService;
        }

        [HttpGet]
        public async Task IndexAsync()
        {

            var access_token = await _loginService.GetManagementApiTokenAsync();
            /*//var id_token = await _loginService.GetAuthenticationToken();

                // Decode the JWT token
    var handler = new JwtSecurityTokenHandler();
    var jwtToken = handler.ReadJwtToken(access_token);
    
        var subject = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

        UserInfo userInfo =  await _loginService.GetUserInfoBySubjectAsync(subject, access_token);


        _logger.LogInformation($"roles: {subject}", userInfo);*/

            HttpContext.Session.SetString("AccessToken", access_token);

            return;
        }

        [HttpGet("post-activation")]
        public IActionResult PostActivation()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userName = User.FindFirstValue(ClaimTypes.Name);

            if (!string.IsNullOrEmpty(userEmail))
            {
                _emailService.SendEmail(userEmail, userName ?? "User");
                return View(); // Renders a post-activation confirmation view
            }

            return BadRequest("User email not found.");

        }


    }
}

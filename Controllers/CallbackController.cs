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
        public async Task<IActionResult> Index()
        {

            var access_token = await _loginService.GetManagementApiTokenAsync();
            //var id_token = await _loginService.GetAuthenticationToken();

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;


            using (var client = new HttpClient())
            {
                // Add the Authorization header with Bearer token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                // Make the authorized requepst
                var response = await client.GetAsync("http://localhost:5012/api/User/index");

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

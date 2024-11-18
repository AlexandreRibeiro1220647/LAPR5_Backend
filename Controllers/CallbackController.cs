using Auth0.ManagementApi.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApi.Services;
using TodoApi.Services.Login;
using TodoApi.DTOs.Auth;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class CallbackController : Controller
    {

        private readonly EmailService _emailService;
        private readonly ILoginService _loginService;
        private readonly IPatientService _patientService;

        public CallbackController(EmailService emailService, ILoginService loginService, IPatientService patientService){
            _emailService = emailService;
            _loginService = loginService;
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult> Callback([FromQuery] string code, [FromQuery] string state)
        {

            var id_token = await _loginService.ExchangeAuthorizationCodeForTokensAsync(code);
            HttpContext.Session.SetString("id_token", id_token);

            await _loginService.MarkSessionAsAuthenticated(state, id_token);

            return Ok();
        }

        [HttpGet("post-activation")]
        public IActionResult PostActivation()
        {
            string id_token = HttpContext.Session.GetString("id_token");

            if (id_token == null) {
                return BadRequest("Authentication error regarding Session.");
            }

            string email = _loginService.GetEmailFromIdToken(id_token);
            var userName = User.FindFirstValue(ClaimTypes.Name);

            if (!string.IsNullOrEmpty(email))
            {
                _emailService.SendEmail(email, userName ?? "User");
                return View();
            }

            return BadRequest("User email not found.");

        }

        [HttpGet("isAuth/{sessionId}")]
        public async Task<bool> CheckIfSessionIsAuthenticated(string sessionId)
        {
            // Query the session data to see if it's marked as authenticated
            var session =  await _loginService.GetSessionByIdAsync(sessionId);
            return session?.IsAuthenticated ?? false;
        }


        [HttpGet("register-patient")]
        public async Task<IActionResult> RegisterPatient([FromQuery] string code, [FromQuery] string state)
        {               
            var id_token = await _loginService.ExchangeAuthorizationCodeForTokensAsync(code);

            HttpContext.Session.SetString("id_token", id_token);

            await _loginService.MarkSessionAsAuthenticated(state, id_token);

            return Ok("Patient registered successfully");
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        private readonly EmailService _emailService;
        private readonly ILoginService _loginService;

        public CallbackController(EmailService emailService, ILoginService loginService){
            _emailService = emailService;
            _loginService = loginService;
        }

        [HttpGet]
        public async Task Callback([FromQuery] string code)
        {

            var id_token = await _loginService.ExchangeAuthorizationCodeForTokensAsync(code);
            HttpContext.Session.SetString("id_token", id_token);

            return;
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


    }
}

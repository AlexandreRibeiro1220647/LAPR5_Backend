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
        private readonly IPatientService _patientService;

        public CallbackController(EmailService emailService, ILoginService loginService, IPatientService patientService){
            _emailService = emailService;
            _loginService = loginService;
            _patientService = patientService;
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

        [HttpGet("register-patient")]
        public async Task<IActionResult> RegisterPatient([FromQuery] string code)
        {   
            string email = HttpContext.Session.GetString("patient_email");
            if (string.IsNullOrEmpty(code))
            {
                await _patientService.DeletePatientByEmailAsync(email);
                HttpContext.Session.Remove("patient_email");
                return BadRequest("Authorization code is missing.");
            }

            var id_token = "";

            try {
                id_token = await _loginService.ExchangeAuthorizationCodeForTokensAsync(code);
                HttpContext.Session.SetString("id_token", id_token);
            } catch (Exception e) {
                await _patientService.DeletePatientByEmailAsync(email);
                HttpContext.Session.Remove("patient_email");
                return BadRequest($"Error exchanging code for tokens.\n{e.ToString}");
            }

            string email_from_id = _loginService.GetEmailFromIdToken(id_token);

            if (!email.Equals(email_from_id)) {
                await _patientService.DeletePatientByEmailAsync(email);
                HttpContext.Session.Remove("patient_email");
                return BadRequest("Email registered in the application is different from the one used to register in the IAM.");
            }

            HttpContext.Session.Remove("patient_email");

            await _loginService.defineIAMRoleAsPatient(id_token);

            return Ok("Patient registered successfully");
        }

    }
}

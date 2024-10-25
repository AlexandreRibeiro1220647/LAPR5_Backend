using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TodoApi.Controllers
{
    [Route("[controller]")]
    public class CallbackController : Controller
    {

        private readonly ILogger<UserController> _logger;

        public CallbackController(ILogger<UserController> logger){
            _logger = logger;
        }

[HttpGet]
public IActionResult Index()
{
    // Retrieve user claims
    var userClaims = User.Claims;

    // Extract roles from the claims
    var roles = userClaims
        .Where(c => c.Type == "http://myapp.com/roles")
        .Select(c => c.Value)
        .ToList();

    _logger.LogInformation("User roles: {roles}", roles);

    // Redirect based on role
    if (roles.Contains("Admin"))
    {
        _logger.LogInformation("Redirecting to User index for Admin role.");
        return RedirectToAction("Index", "User");
    }
    else if (roles.Contains("Staff"))
    {
        _logger.LogInformation("Redirecting to Staff index for Staff role.");
        return RedirectToAction("Index", "Staff");
    }
    else
    {
        _logger.LogInformation("Redirecting to Home index as default.");
        return RedirectToAction("Index", "Home");
    }
}

    }
}

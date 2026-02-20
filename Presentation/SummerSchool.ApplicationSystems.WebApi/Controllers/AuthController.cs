using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SummerSchool.ApplicationSystems.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[AllowAnonymous]
public class AuthController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}

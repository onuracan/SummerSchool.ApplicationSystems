using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Common.Constants;

namespace SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(AuthenticationSchemes = AdminCookieConstants.SCHEME)]
public class HomeController(IHttpContextAccessor httpContextAccessor,
                            IHttpClientFactory httpClientFactory) : BaseAdminController(httpContextAccessor, httpClientFactory)
{
    [HttpGet(AdminRouteConstants.INDEX)]
    public IActionResult Index()
    {
        return View();
    }
}

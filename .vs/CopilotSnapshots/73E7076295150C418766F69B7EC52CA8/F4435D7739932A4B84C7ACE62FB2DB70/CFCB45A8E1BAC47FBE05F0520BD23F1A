using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Common.Constants;

namespace SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Controllers;

public class HomeController(IHttpContextAccessor httpContextAccessor,
                            IHttpClientFactory httpClientFactory) 
    : BaseAdminController(httpContextAccessor, httpClientFactory)
{
    [HttpGet(RouteConstants.ADMIN_INDEX)]
    public IActionResult Index()
    {
        return View();
    }
}

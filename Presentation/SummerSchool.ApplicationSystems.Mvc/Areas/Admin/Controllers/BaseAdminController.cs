using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Common.Constants;

namespace SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(AuthenticationSchemes = CookieAuthenticationConstants.ADMIN_SCHEME)]
public abstract class BaseAdminController(IHttpContextAccessor httpContextAccessor,
                                          IHttpClientFactory httpClientFactory) 
    : BaseController(httpContextAccessor, httpClientFactory)
{
}

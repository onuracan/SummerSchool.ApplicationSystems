using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Common.Constants;
using SummerSchool.ApplicationSystems.Mvc.Models.Course.Response;
using static SummerSchool.ApplicationSystems.Mvc.Common.Constants.RouteConstants;

namespace SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(AuthenticationSchemes = CookieAuthenticationConstants.ADMIN_SCHEME)]
public class CourseController(IHttpContextAccessor httpContextAccessor,
                            IHttpClientFactory httpClientFactory) : BaseAdminController(httpContextAccessor, httpClientFactory)
{

    [HttpGet(RouteConstants.ADMIN_GET_COURSES)]
    public async Task<IActionResult> GetCourses()
    {
        var response = await this.GetApiRequestAsync<List<CourseListResponseViewModel>>(ApiEndpoints.GET_COURSES).ConfigureAwait(false);

        return Json(response);
    }
}

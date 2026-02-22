using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Common.Constants;
using SummerSchool.ApplicationSystems.Mvc.Common.Constants;
using SummerSchool.ApplicationSystems.Mvc.Models.Course.Response;

namespace SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(AuthenticationSchemes = AdminCookieConstants.SCHEME)]
public class CourseController(IHttpContextAccessor httpContextAccessor,
                            IHttpClientFactory httpClientFactory) : BaseAdminController(httpContextAccessor, httpClientFactory)
{

    [HttpGet(AdminRouteConstants.GET_COURSES)]
    public async Task<IActionResult> GetCourses()
    {
        var response = await this.GetApiRequestAsync<List<CourseListResponseViewModel>>(StudentApiEndpoints.GET_COURSES).ConfigureAwait(false);

        return Json(response);
    }
}

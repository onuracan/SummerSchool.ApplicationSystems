using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Common.Constants;
using SummerSchool.ApplicationSystems.Mvc.Models.CourseApplication.Response;
using static SummerSchool.ApplicationSystems.Mvc.Common.Constants.RouteConstants;

namespace SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(AuthenticationSchemes = CookieAuthenticationConstants.ADMIN_SCHEME)]
public class CourseApplicationController(IHttpContextAccessor httpContextAccessor,
                                         IHttpClientFactory httpClientFactory) : BaseAdminController(httpContextAccessor, httpClientFactory)
{

    [HttpGet(RouteConstants.ADMIN_GET_APPLICATIONS)]
    public async Task<IActionResult> GetApplicationsByCourseId(Guid courseId)
    {
        var response = await this.GetApiRequestAsync<IEnumerable<CourseApplicationListResponseViewModel>>(string.Format(ApiEndpoints.APPLICATIONS_BY_COURSE, courseId.ToString())).ConfigureAwait(false);

        return Json(response);
    }
}

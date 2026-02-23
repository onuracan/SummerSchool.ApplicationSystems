using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Common.Constants;
using SummerSchool.ApplicationSystems.Mvc.Common.Constants;
using SummerSchool.ApplicationSystems.Mvc.Models.CourseApplication.Response;

namespace SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(AuthenticationSchemes = AdminCookieConstants.SCHEME)]
public class CourseApplicationController(IHttpContextAccessor httpContextAccessor,
                                         IHttpClientFactory httpClientFactory) : BaseAdminController(httpContextAccessor, httpClientFactory)
{

    [HttpGet(AdminRouteConstants.GET_APPLICATIONS)]
    public async Task<IActionResult> GetApplicationsByCourseId(Guid courseId)
    {
        var response = await this.GetApiRequestAsync<IEnumerable<CourseApplicationListResponseViewModel>>(string.Format(AdminApiEndpoints.APPLICATIONS_BY_COURSE, courseId.ToString())).ConfigureAwait(false);

        return Json(response);
    }
}

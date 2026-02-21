using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Common.Constants;
using SummerSchool.ApplicationSystems.Mvc.Models.CourseApplication.Response;

namespace SummerSchool.ApplicationSystems.Mvc.Controllers;

[Authorize]
public class CourseApplicationController(IHttpContextAccessor httpContextAccessor,
                                         IHttpClientFactory httpClientFactory) : BaseController(httpContextAccessor, httpClientFactory)
{

    [HttpGet(RouteConstants.GET_ME_APPLICATIONS)]
    public async Task<IActionResult> GetMeApplications()
    {
        var response = await this.GetApiRequestAsync<IEnumerable<CourseApplicationListResponseViewModel>>(ApiEndpoints.MY_APPLICATIONS).ConfigureAwait(false);

        return Json(response);
    }

    [HttpPost(RouteConstants.APPLY_COURSE)]
    public async Task<IActionResult> ApplyCourse([FromBody] Guid courseId)
    {
        var response = await this.PostApiRequestAsync(ApiEndpoints.COURSE_APPLICATIONS, new { courseId }).ConfigureAwait(false);

        return Json(response);
    }
}

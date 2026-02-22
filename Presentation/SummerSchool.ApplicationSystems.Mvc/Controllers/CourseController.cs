using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Common.Constants;
using SummerSchool.ApplicationSystems.Mvc.Models.Course.Response;

namespace SummerSchool.ApplicationSystems.Mvc.Controllers;

[Authorize(AuthenticationSchemes = StudentCookieConstants.SCHEME)]
public class CourseController(IHttpContextAccessor httpContextAccessor,
                            IHttpClientFactory httpClientFactory) : BaseController(httpContextAccessor, httpClientFactory)
{

    [HttpGet(StudentRouteConstants.GET_COURSES)]
    public async Task<IActionResult> GetCourses()
    {
        var response = await this.GetApiRequestAsync<List<CourseListResponseViewModel>>(StudentApiEndpoints.GET_COURSES).ConfigureAwait(false);

        return Json(response);
    }
}

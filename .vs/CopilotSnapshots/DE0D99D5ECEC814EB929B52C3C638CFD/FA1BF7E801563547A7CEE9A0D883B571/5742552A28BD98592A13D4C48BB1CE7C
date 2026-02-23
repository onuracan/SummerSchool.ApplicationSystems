using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Models.CourseApplication.Request;
using SummerSchool.ApplicationSystems.Mvc.Common.Constants;
using SummerSchool.ApplicationSystems.Mvc.Models.Course.Response;
using static SummerSchool.ApplicationSystems.Mvc.Common.Constants.RouteConstants;

namespace SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(AuthenticationSchemes = CookieAuthenticationConstants.ADMIN_SCHEME)]
public class ApplicationController(IHttpContextAccessor httpContextAccessor,
                                   IHttpClientFactory httpClientFactory) : BaseAdminController(httpContextAccessor, httpClientFactory)
{
    [HttpGet(RouteConstants.ADMIN_APP_INDEX)]
    public async Task<IActionResult> Index()
    {
        var response = await this.GetApiRequestAsync<IEnumerable<CourseDropdownListResponseViewModel>>(ApiEndpoints.GET_COURSE_DROPDOWNLIST).ConfigureAwait(false);

        ViewBag.Response = response;

        return View();
    }

    [HttpPut(RouteConstants.ADMIN_UPDATE_APPLICATION_STATUS)]
    public async Task<IActionResult> UpdateApplicationStatus([FromRoute] Guid id, [FromBody] UpdateCourseApplicationStatusRequestViewModel request)
    {
        var response = await this.PutApiRequestAsync(string.Format(ApiEndpoints.UPDATE_APPLICATION_STATUS, id.ToString()), request).ConfigureAwait(false);

        return Json(response);
    }
}

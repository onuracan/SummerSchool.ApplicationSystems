using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Common.Constants;
using SummerSchool.ApplicationSystems.Mvc.Models.Student.Request;

namespace SummerSchool.ApplicationSystems.Mvc.Controllers;

[Authorize]
public class StudentController(IHttpContextAccessor httpContextAccessor,
                            IHttpClientFactory httpClientFactory) : BaseController(httpContextAccessor, httpClientFactory)
{
    [HttpPost(RouteConstants.CREATE_STUDENT)]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequestModel request)
    {
        var response = await this.PostApiRequestAsync(ApiEndpoints.ADD_STUDENT, request).ConfigureAwait(false);

        return Json(response);
    }

    [HttpPut($"{RouteConstants.UPDATE_STUDENT}/{{id}}")]
    public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, [FromBody] UpdateStudentRequestModel request)
    {
        request.Id = id;

        var endpoint = string.Format(ApiEndpoints.UPDATE_STUDENT, id);
        var response = await this.PutApiRequestAsync(endpoint, request).ConfigureAwait(false);

        return Json(response);
    }
}

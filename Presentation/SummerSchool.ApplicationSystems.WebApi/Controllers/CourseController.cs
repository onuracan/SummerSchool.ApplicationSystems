using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Core.DTOs.Course.Request;
using SummerSchool.ApplicationSystems.Core.Services.Course;
using System.Net;

namespace SummerSchool.ApplicationSystems.WebApi.Controllers;

[ApiController]
[Route("api/")]
[Produces("application/json")]
[Authorize]
public class CourseController(ICourseQueryService courseQueryService,
                              ICourseCommandService courseCommandService,
                              IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly ICourseQueryService _courseQueryService = courseQueryService;
    private readonly ICourseCommandService _courseCommandService = courseCommandService;

    [HttpGet("courses")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCourses(CancellationToken cancellationToken)
    {
        var response = await this._courseQueryService.GetCoursesAsync(cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    [HttpGet("courseDropdownList")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCourseDropdownList(CancellationToken cancellationToken)
    {
        var response = await this._courseQueryService.GetCourseDropdownListAsync(cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    [HttpPost("courses")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequestDto request, CancellationToken cancellationToken)
    {
        var response = await this._courseCommandService.CreateCourseAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    [HttpPut("courses/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateCourse([FromRoute] Guid id, [FromBody] UpdateCourseRequestDto request, CancellationToken cancellationToken)
    {
        request.Id = id;

        var response = await this._courseCommandService.UpdateCourseAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }
}

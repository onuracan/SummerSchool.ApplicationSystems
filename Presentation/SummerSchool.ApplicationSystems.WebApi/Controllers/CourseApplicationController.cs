using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Core.DTOs.CourseApplication.Request;
using SummerSchool.ApplicationSystems.Core.Services.CourseApplication;
using System.Net;

namespace SummerSchool.ApplicationSystems.WebApi.Controllers;

[ApiController]
[Route("api/")]
[Produces("application/json")]
[Authorize]
public class CourseApplicationController(ICourseApplicationQueryService courseApplicationQueryService,
                                         ICourseApplicationCommandService courseApplicationCommandService,
                                         IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly ICourseApplicationQueryService _courseApplicationQueryService = courseApplicationQueryService;
    private readonly ICourseApplicationCommandService _courseApplicationCommandService = courseApplicationCommandService;

    [HttpGet("me/applications")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCourseApplicationsByStudentId(CancellationToken cancellationToken)
    {
        var response = await this._courseApplicationQueryService.GetCourseApplicationsByStudentIdAsync(cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    [HttpGet("courses/{courseId}/applications")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCourseApplicationsByCourseId([FromRoute] Guid courseId, CancellationToken cancellationToken)
    {
        var response = await this._courseApplicationQueryService.GetCourseApplicationsByCourseIdAsync(courseId, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    [HttpPost("course-applications")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateCourseApplication([FromBody] CreateCourseApplicationRequestDto request, CancellationToken cancellationToken)
    {
        var response = await this._courseApplicationCommandService.CreateCourseApplicationAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    [HttpPut("course-applications/{id}/status")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateApplicationStatus([FromRoute] Guid id, [FromBody] UpdateCourseApplicationStatusRequestDto request, CancellationToken cancellationToken)
    {
        request.Id = id;

        var response = await this._courseApplicationCommandService.UpdateApplicationStatusAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }
}

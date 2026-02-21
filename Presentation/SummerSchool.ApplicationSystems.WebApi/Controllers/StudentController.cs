using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Core.DTOs.Student.Request;
using SummerSchool.ApplicationSystems.Core.Services.Student;
using System.Net;

namespace SummerSchool.ApplicationSystems.WebApi.Controllers;

[ApiController]
[Route("api/")]
[Produces("application/json")]
[Authorize]
public class StudentController(IStudentQueryService studentQueryService,
                               IStudentCommandService studentCommandService, 
                               IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly IStudentQueryService _studentQueryService = studentQueryService;
    private readonly IStudentCommandService _studentCommandService = studentCommandService;

    [HttpGet("student/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await this._studentQueryService.GetById(id, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    [HttpPost("students")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequestDto request, CancellationToken cancellationToken)
    {
        var response = await this._studentCommandService.CreateStudentAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    [HttpPut("students/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, [FromBody] UpdateStudentRequestDto request, CancellationToken cancellationToken)
    {
        request.Id = id;

        var response = await this._studentCommandService.UpdateStudentAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }
}

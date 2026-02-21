using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Core.DTOs.Auth.Request;
using SummerSchool.ApplicationSystems.Core.DTOs.OtpVerification.Request;
using SummerSchool.ApplicationSystems.Core.Services.Auth;
using SummerSchool.ApplicationSystems.Core.Services.OtpVerification;
using System.Net;

namespace SummerSchool.ApplicationSystems.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[AllowAnonymous]
public class AuthController(IOtpVerificationService otpVerificationService,
                            IAuthService authService,
                            IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly IOtpVerificationService _otpVerificationService = otpVerificationService;
    private readonly IAuthService _authService = authService;

    [HttpPost("request-otp")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> RequestOtp([FromBody] CreateOtpRequestDto request, CancellationToken cancellationToken)
    {
        var response = await this._otpVerificationService.CreateOtpAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    [HttpPost("verify-otp")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> VerifyOtp([FromBody] string code)
    {
        var response = this._otpVerificationService.VerifyOtp(code);

        return this.CreateJsonResponse(response);
    }

    [HttpPost("student-login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> StudentLogin([FromBody] StudentLoginRequestDto request, CancellationToken cancellationToken)
    {
        var response = await this._authService.StudentLoginAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    [HttpPost("admin-login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> AdminLogin([FromBody] AdminLoginRequestDto request)
    {
        var response = this._authService.AdminLogin(request);

        return this.CreateJsonResponse(response);
    }
}

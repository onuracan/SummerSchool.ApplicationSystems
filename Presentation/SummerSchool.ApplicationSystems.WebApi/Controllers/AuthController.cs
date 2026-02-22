using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Core.DTOs.Auth.Request;
using SummerSchool.ApplicationSystems.Core.DTOs.OtpVerification.Request;
using SummerSchool.ApplicationSystems.Core.Services.Auth;
using SummerSchool.ApplicationSystems.Core.Services.OtpVerification;
using System.Net;

namespace SummerSchool.ApplicationSystems.WebApi.Controllers;

/// <summary>
/// Kimlik doğrulama işlemlerini yöneten controller
/// </summary>
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

    /// <summary>
    /// GSM numarasına OTP (SMS) kodu gönderir
    /// </summary>
    /// <param name="request">GSM numarası içeren istek</param>
    /// <param name="cancellationToken">İptal token</param>
    /// <returns>OTP gönderim sonucu</returns>
    /// <remarks>
    /// Örnek istek:
    /// 
    ///     POST /api/auth/request-otp
    ///     {
    ///         "phoneNumber": "5551234567"
    ///     }
    /// 
    /// Test ortamında sabit kod: 147852
    /// </remarks>
    /// <response code="200">OTP başarıyla gönderildi</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpPost("request-otp")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> RequestOtp([FromBody] CreateOtpRequestDto request, CancellationToken cancellationToken)
    {
        var response = await this._otpVerificationService.CreateOtpAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    /// <summary>
    /// Gönderilen OTP kodunu doğrular
    /// </summary>
    /// <param name="code">6 haneli doğrulama kodu</param>
    /// <returns>Doğrulama sonucu</returns>
    /// <remarks>
    /// Örnek istek:
    /// 
    ///     POST /api/auth/verify-otp
    ///     "147852"
    /// 
    /// </remarks>
    /// <response code="200">OTP kodu doğrulandı</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpPost("verify-otp")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> VerifyOtp([FromBody] string code)
    {
        var response = this._otpVerificationService.VerifyOtp(code);

        return this.CreateJsonResponse(response);
    }

    /// <summary>
    /// Öğrenci girişi yapar ve JWT token döner
    /// </summary>
    /// <param name="request">GSM numarası içeren giriş isteği</param>
    /// <param name="cancellationToken">İptal token</param>
    /// <returns>JWT token ve kullanıcı bilgileri</returns>
    /// <remarks>
    /// OTP doğrulamasından sonra bu endpoint ile giriş yapılır.
    /// 
    /// Örnek istek:
    /// 
    ///     POST /api/auth/student-login
    ///     {
    ///         "phoneNumber": "5551234567"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Giriş başarılı, JWT token döner</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpPost("student-login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> StudentLogin([FromBody] StudentLoginRequestDto request, CancellationToken cancellationToken)
    {
        var response = await this._authService.StudentLoginAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    /// <summary>
    /// Yönetici girişi yapar ve JWT token döner
    /// </summary>
    /// <param name="request">Kullanıcı adı ve şifre içeren giriş isteği</param>
    /// <returns>JWT token ve kullanıcı bilgileri</returns>
    /// <remarks>
    /// Admin kullanıcıları için giriş endpoint'i.
    /// 
    /// Örnek istek:
    /// 
    ///     POST /api/auth/admin-login
    ///     {
    ///         "userName": "admin",
    ///         "password": "adminhalic"
    ///     }
    /// 
    /// Test Kullanıcısı:
    /// - Kullanıcı Adı: admin
    /// - Şifre: adminhalic
    /// </remarks>
    /// <response code="200">Giriş başarılı, JWT token döner</response>
    /// <response code="400">Kullanıcı adı veya şifre hatalı</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpPost("admin-login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> AdminLogin([FromBody] AdminLoginRequestDto request)
    {
        var response = this._authService.AdminLogin(request);

        return this.CreateJsonResponse(response);
    }
}

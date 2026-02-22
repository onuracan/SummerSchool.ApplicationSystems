using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Common.Constants;
using SummerSchool.ApplicationSystems.Mvc.Models.OtpVerification.Response;
using SummerSchool.ApplicationSystems.Shared.Models;
using System.Security.Claims;

namespace SummerSchool.ApplicationSystems.Mvc.Controllers;

[AllowAnonymous]
public class AuthController(IHttpContextAccessor httpContextAccessor,
                            IHttpClientFactory httpClientFactory) : BaseController(httpContextAccessor, httpClientFactory)
{
    [HttpGet(StudentRouteConstants.AUTH_LOGIN)]
    public IActionResult Login()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost(StudentRouteConstants.AUTH_REQUEST_OTP)]
    public async Task<JsonResult> RequestOtp([FromBody] string phoneNumber)
    {
        var response = await this.PostApiRequestAsync<OtpVerificationResponseModel>(StudentApiEndpoints.AUTH_REQUEST_OTP, new { phoneNumber }).ConfigureAwait(false);

        return Json(response);
    }

    [ValidateAntiForgeryToken]
    [HttpPost(StudentRouteConstants.AUTH_VERIFY_OTP)]
    public async Task<JsonResult> VerifyOtp([FromBody] string code)
    {
        var response = await this.PostApiRequestAsync<OtpVerificationResponseModel>(StudentApiEndpoints.AUTH_VERIFY_OTP, code).ConfigureAwait(false);

        return Json(response);
    }

    [ValidateAntiForgeryToken]
    [HttpPost(StudentRouteConstants.AUTH_LOGIN)]
    public async Task<JsonResult> Login([FromBody] string phoneNumber)
    {
        var response = await this.PostApiRequestAsync<UserModel>(StudentApiEndpoints.AUTH_STUDENT_LOGIN, new { phoneNumber }).ConfigureAwait(false);
        if (!response.IsSuccessful)
            return Json(response);

        var claims = this.GetUserClaims(response.Result);

        var claimsIdentity = new ClaimsIdentity(claims, StudentCookieConstants.SCHEME);

        var properties = new AuthenticationProperties()
        {
            IsPersistent = true,
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
        };

        await HttpContext.SignInAsync(StudentCookieConstants.SCHEME, new ClaimsPrincipal(claimsIdentity), properties);

        return Json(response);
    }

    [HttpGet(StudentRouteConstants.AUTH_LOGOUT)]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(StudentCookieConstants.SCHEME);

        return Redirect(StudentRouteConstants.AUTH_LOGIN);
    }

    [HttpGet(StudentRouteConstants.ACCESS_DENIED)]
    public IActionResult AccessDenied()
    {
        return View();
    }

    public List<Claim> GetUserClaims(UserModel user)
    {
        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("UserType", user.UserType.ToString())
        };

        if (!string.IsNullOrEmpty(user.UserName))
            claims.Add(new Claim("UserName", user.UserName));

        if (!string.IsNullOrEmpty(user.NameAndSurname))
            claims.Add(new Claim("NameAndSurname", user.NameAndSurname));

        if (!string.IsNullOrEmpty(user.PhoneNumber))
            claims.Add(new Claim("PhoneNumber", user.PhoneNumber));

        if (!string.IsNullOrEmpty(user.EMail))
            claims.Add(new Claim("EMail", user.EMail));

        if (!string.IsNullOrEmpty(user.AccessToken))
            claims.Add(new Claim("AccessToken", user.AccessToken));

        if (user.Expiration != default)
            claims.Add(new Claim("Expiration", user.Expiration.ToString("o")));

        return claims;
    }
}

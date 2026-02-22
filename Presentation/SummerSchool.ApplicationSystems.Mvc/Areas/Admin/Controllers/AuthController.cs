using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Models.Auth;
using SummerSchool.ApplicationSystems.Mvc.Common.Constants;
using SummerSchool.ApplicationSystems.Shared.Models;
using System.Security.Claims;
using static SummerSchool.ApplicationSystems.Mvc.Common.Constants.RouteConstants;

namespace SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[AllowAnonymous]
public class AuthController(IHttpContextAccessor httpContextAccessor,
                            IHttpClientFactory httpClientFactory) : BaseAdminController(httpContextAccessor, httpClientFactory)
{
    [HttpGet(RouteConstants.ADMIN_LOGIN)]
    public IActionResult Login()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost(RouteConstants.ADMIN_LOGIN)]
    public async Task<JsonResult> Login([FromBody] AdminLoginRequestViewModel request)
    {
        var response = await this.PostApiRequestAsync<UserModel>(ApiEndpoints.AUTH_ADMIN_LOGIN, request).ConfigureAwait(false);
        if (!response.IsSuccessful)
            return Json(response);

        var claims = this.GetUserClaims(response.Result);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationConstants.ADMIN_SCHEME);

        var properties = new AuthenticationProperties()
        {
            IsPersistent = true,
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
        };

        await HttpContext.SignInAsync(CookieAuthenticationConstants.ADMIN_SCHEME, new ClaimsPrincipal(claimsIdentity), properties);

        return Json(response);
    }

    [HttpGet(RouteConstants.ADMIN_LOGOUT)]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationConstants.ADMIN_SCHEME);

        Response.Cookies.Delete(
            CookieAuthenticationConstants.ADMIN_COOKIE_NAME,
            new CookieOptions
            {
                Path = CookieAuthenticationConstants.ADMIN_COOKIE_PATH,
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                IsEssential = true
            }
        );

        return Redirect(RouteConstants.ADMIN_LOGIN);
    }
    
    [HttpGet(RouteConstants.ADMIN_ACCESS_DENIED)]
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

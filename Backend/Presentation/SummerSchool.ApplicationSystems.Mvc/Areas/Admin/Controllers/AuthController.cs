using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Common.Constants;
using SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Models.Auth;
using SummerSchool.ApplicationSystems.Shared.Models;
using System.Security.Claims;

namespace SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[AllowAnonymous]
public class AuthController(IHttpContextAccessor httpContextAccessor,
                            IHttpClientFactory httpClientFactory) : BaseAdminController(httpContextAccessor, httpClientFactory)
{
    [HttpGet(AdminRouteConstants.LOGIN)]
    public IActionResult Login()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost(AdminRouteConstants.LOGIN)]
    public async Task<JsonResult> Login([FromBody] AdminLoginRequestViewModel request)
    {
        var response = await this.PostApiRequestAsync<UserModel>(AdminApiEndpoints.AUTH_LOGIN, request).ConfigureAwait(false);
        if (!response.IsSuccessful)
            return Json(response);

        var claims = this.GetUserClaims(response.Result);

        var claimsIdentity = new ClaimsIdentity(claims, AdminCookieConstants.SCHEME);

        var properties = new AuthenticationProperties()
        {
            IsPersistent = true,
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
        };

        await HttpContext.SignInAsync(AdminCookieConstants.SCHEME, new ClaimsPrincipal(claimsIdentity), properties);

        return Json(response);
    }

    [HttpGet(AdminRouteConstants.LOGOUT)]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(AdminCookieConstants.SCHEME);

        Response.Cookies.Delete(
            AdminCookieConstants.COOKIE_NAME,
            new CookieOptions
            {
                Path = AdminCookieConstants.COOKIE_PATH,
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                IsEssential = true
            }
        );

        return Redirect(AdminRouteConstants.LOGIN);
    }

    [HttpGet(AdminRouteConstants.ACCESS_DENIED)]
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

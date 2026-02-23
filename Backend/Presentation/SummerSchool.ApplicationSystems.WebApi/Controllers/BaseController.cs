using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Shared.Enums;
using SummerSchool.ApplicationSystems.Shared.Models;
using System.Security.Claims;

namespace SummerSchool.ApplicationSystems.WebApi.Controllers;

public class BaseController : Controller
{
    public UserModel UserInfo { get; set; }

    protected BaseController(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            ClaimsPrincipal user = httpContextAccessor.HttpContext.User;
            UserInfo = new UserModel();

            var idClaim = user.FindFirst("Id")?.Value;
            if (idClaim != null && Guid.TryParse(idClaim, out var userId))
                UserInfo.Id = userId;

            var userNameClaim = user.FindFirst("UserName")?.Value;
            if (!string.IsNullOrEmpty(userNameClaim))
                UserInfo.UserName = userNameClaim;

            var nameSurnameClaim = user.FindFirst("NameAndSurname")?.Value;
            if (!string.IsNullOrEmpty(nameSurnameClaim))
                UserInfo.NameAndSurname = nameSurnameClaim;

            var phoneNumberClaim = user.FindFirst("PhoneNumber")?.Value;
            if (!string.IsNullOrEmpty(phoneNumberClaim))
                UserInfo.PhoneNumber = phoneNumberClaim;

            var emailClaim = user.FindFirst("EMail")?.Value;
            if (!string.IsNullOrEmpty(emailClaim))
                UserInfo.EMail = emailClaim;

            var accessTokenClaim = user.FindFirst("AccessToken")?.Value;
            if (!string.IsNullOrEmpty(accessTokenClaim))
                UserInfo.AccessToken = accessTokenClaim;

            var expirationClaim = user.FindFirst("Expiration")?.Value;
            if (!string.IsNullOrEmpty(expirationClaim) && DateTime.TryParse(expirationClaim, out var expiration))
                UserInfo.Expiration = expiration;

            var userTypeClaim = user.FindFirst("UserType")?.Value;
            if (!string.IsNullOrEmpty(userTypeClaim) && Enum.TryParse<UserType>(userTypeClaim, out var userType))
                UserInfo.UserType = userType;
        }
    }

    [NonAction]
    protected JsonResult CreateJsonResponse(ServiceResponseDto response)
    {
        return new JsonResult(response)
        {
            StatusCode = StatusCodes.Status200OK,
            ContentType = "application/json"
        };
    }

    [NonAction]
    protected JsonResult CreateJsonResponse<T>(ServiceResponseDto<T> response)
    {
        return new JsonResult(response)
        {
            StatusCode = StatusCodes.Status200OK,
            ContentType = "application/json"
        };
    }

    [NonAction]
    protected JsonResult CreateObjectResponse(ServiceResponseDto response)
    {
        return new JsonResult(response)
        {
            StatusCode = StatusCodes.Status200OK,
        };
    }

    [NonAction]
    protected JsonResult CreateObjectResponse<T>(ServiceResponseDto<T> response)
    {
        return new JsonResult(response)
        {
            StatusCode = StatusCodes.Status200OK,
        };
    }
}

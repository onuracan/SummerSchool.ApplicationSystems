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
            UserInfo = new UserModel
            {
                UserName = user.FindFirst("UserName")?.Value,
                NameAndSurname = user.FindFirst("NameAndSurname")?.Value,
                PhoneNumber = user.FindFirst("PhoneNumber")?.Value,
                EMail = user.FindFirst("EMail")?.Value,
                AccessToken = user.FindFirst("AccessToken")?.Value,
                Expiration = DateTime.Parse(user.FindFirst("Expiration")?.Value),
                UserType = Enum.Parse<UserType>(user.FindFirst("UserType")?.Value)
            };
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

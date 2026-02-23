using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Mvc.Common.Constants;
using System.Net;

namespace SummerSchool.ApplicationSystems.Mvc.Controllers;

[AllowAnonymous]
public class ErrorController(ILogger<ErrorController> logger) : Controller
{
    private readonly ILogger<ErrorController> _logger = logger;

    [HttpGet(StudentRouteConstants.ERROR)]
    public IActionResult Handler(int statusCode)
    {
        HttpContext.Response.StatusCode = statusCode;

        if (statusCode == (int)HttpStatusCode.InternalServerError)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (feature != null && feature.Error != null)
            {
                var errorMessage = $"{feature.Error.Message.ToString()} <br /> {(feature.Error.InnerException != null ? feature.Error.InnerException.Message.ToString() : "")} <br /> {feature.Error.StackTrace}";

                this._logger.LogError(errorMessage);

                ViewData["Errors"] = errorMessage;
            }
        }

        var viewName = statusCode == StatusCodes.Status404NotFound ? "NotFound" : "ServerFault";

        return View(viewName);
    }
}

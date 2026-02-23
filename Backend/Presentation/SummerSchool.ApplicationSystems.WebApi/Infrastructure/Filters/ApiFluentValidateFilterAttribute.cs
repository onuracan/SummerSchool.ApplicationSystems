using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using System.Text;

namespace SummerSchool.ApplicationSystems.WebApi.Infrastructure.Filters;

public class ApiFluentValidateFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        if (context.ModelState.IsValid)
            return;

        var errorBuilder = new StringBuilder();

        foreach (var error in context.ModelState.Values.SelectMany(x => x.Errors))
        {
            errorBuilder.AppendLine($"{error.ErrorMessage}");
        }

        var response = ServiceResponseDto.SetFail(StatusCodes.Status400BadRequest, errorBuilder.ToString());

        context.Result = new JsonResult(response);
    }
}

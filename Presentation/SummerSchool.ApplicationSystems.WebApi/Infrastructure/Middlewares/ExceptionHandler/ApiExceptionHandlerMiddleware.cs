using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using System.Text.Json;

namespace SummerSchool.ApplicationSystems.WebApi.Infrastructure.Middlewares.ExceptionHandler;

public class ApiExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        string actionParams = string.Empty;

        try
        {
            actionParams = await this.GetActionParamsFromRequestAsync(context);

            await _next(context);
        }
        catch (Exception ex)
        {
            //await this.HandleExceptionAsync(context, ex, actionParams);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var response = ServiceResponseDto.SetFail(statusCode: context.Response.StatusCode, message: ex.Message);
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

    //private async Task HandleExceptionAsync(HttpContext context, Exception ex, string actionParams)
    //{
    //    var errorLogsService = context.RequestServices.GetRequiredService<IErrorLogsService>();

    //    await errorLogsService.CreateAsync(new()
    //    {
    //        AppName = Constants.APP_NAME,
    //        RequestUrl = context.Request.Path,
    //        RequestInput = actionParams,
    //        Message = $"{ex.Message} {ex.InnerException?.Message}",
    //        StackTrace = JsonSerializer.Serialize($"{ex.StackTrace} {ex.InnerException?.StackTrace}", CommonMethods.GetDefaultJsonSerializerOptions()),
    //        AppType = Constants.APP_TYPE,
    //        InsertedDate = DateTime.Now
    //    });
    //}

    private async Task<string> GetActionParamsFromRequestAsync(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Post)
        {
            context.Request.EnableBuffering();
            var paramString = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;
            return paramString;
        }
        else
        {
            return JsonSerializer.Serialize(context.Request.Query);
        }
    }
}

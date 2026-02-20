using SummerSchool.ApplicationSystems.WebApi.Infrastructure.Middlewares.ExceptionHandler;
using SummerSchool.ApplicationSystems.WebApi.Infrastructure.Middlewares.Security;

namespace SummerSchool.ApplicationSystems.WebApi.Infrastructure.Middlewares;

public static class MiddlewareExtensions
{
    public static void UseSecurity(this WebApplication app)
    {
        app.UseMiddleware<SecurityMiddleware>();
    }

    public static void UseApiExceptionHandler(this WebApplication app)
    {
        app.UseMiddleware<ApiExceptionHandlerMiddleware>();
    }
}

namespace SummerSchool.ApplicationSystems.WebApi.Infrastructure.Middlewares.Security;

public class SecurityMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.Headers.TryAdd("X-Xss-Protection", "1; mode-block");
        context.Request.Headers.TryAdd("X-Content-Type-Options", "nosniff");
        context.Request.Headers.TryAdd("X-Frame-Options", "DENY");
        context.Request.Headers.TryAdd("Referrer-Policy", "no-referrer");
        context.Request.Headers.TryAdd("Feature-Policy", "camera 'none'; accelerometer 'none'; geolocation 'none'; magnetometer 'none'; microphone 'none'; usb 'none'");
        context.Request.Headers.TryAdd("X-Permitted-Cross-Domain-Policies", "none");
        context.Response.Headers.Remove("Server");
        context.Response.Headers.Remove("X-Powered-By");
        context.Response.Headers.Remove("X-AspNet-Version");
        context.Response.Headers.Remove("X-AspNetMvc-Version");
        await this._next(context);
    }
}

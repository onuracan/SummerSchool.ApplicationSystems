using Autofac;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using SummerSchool.ApplicationSystems.Core.Options;
using SummerSchool.ApplicationSystems.Repository.Infrastructure;
using SummerSchool.ApplicationSystems.Service.Infrastructure.Configurations;
using SummerSchool.ApplicationSystems.Service.Infrastructure.Extensions;
using SummerSchool.ApplicationSystems.Service.Validators.Auth;
using SummerSchool.ApplicationSystems.Shared.Enums;
using SummerSchool.ApplicationSystems.WebApi.Infrastructure.Extensions;
using SummerSchool.ApplicationSystems.WebApi.Infrastructure.Middlewares;
using SummerSchool.ApplicationSystems.WebApi.Infrastructure.Modules;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

ServicesSection(builder.Services);
var app = builder.Build();
UseSection(app);
app.Run();

void ServicesSection(IServiceCollection services)
{
    services.AddSerilog();
    services.AddControllers().AddCoreJsonOptions().AddApiFluentValidateFilter();
    services.AddApiBehaivorConfigure();
    services.AddCoreFluentValidation<AdminLoginRequestDtoValidator>();
    services.AddHttpContextAccessor();
    services.AddEndpointsApiExplorer();
    if (builder.Environment.IsDevelopment())
        services.AddCustomSwagger();

    services.AddCustomCors(builder.Configuration);
    services.AddRegisterDbContext(builder.Configuration);
    services.AddCountryInfoSoap();
    services.AddJwtAuthentication(builder.Configuration);
    services.AddAutoMapper();

    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule<AutofacModule>());
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

    builder.Services.AddScoped(provider =>
    {
        var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
        var userOptions = new UserOptions();
        var user = httpContextAccessor.HttpContext?.User;
        var identity = user?.Identity as ClaimsIdentity;

        if (identity != null && identity.IsAuthenticated)
        {
            userOptions.Id = Guid.Parse(identity.FindFirst("Id").Value);
            userOptions.UserName = identity.FindFirst("UserName")?.Value;
            Enum.TryParse<UserType>(identity.FindFirst("UserType").Value, out var userType);
            userOptions.UserType = userType;
        }

        return userOptions;
    });

    services.Configure<JwtIssuerSettings>(builder.Configuration.GetSection("JwtIssuerSettings"));
}

void UseSection(WebApplication app)
{
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
        app.UseCustomSwagger();
    else
        app.UseHttpsRedirection();

    app.UseApiExceptionHandler();
    app.UseCors("AllowAll");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}

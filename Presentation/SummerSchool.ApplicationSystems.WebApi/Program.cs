using Autofac;
using Autofac.Extensions.DependencyInjection;
using SummerSchool.ApplicationSystems.Repository.Infrastructure;
using SummerSchool.ApplicationSystems.WebApi.Infrastructure.Extensions;
using SummerSchool.ApplicationSystems.WebApi.Infrastructure.Middlewares;
using SummerSchool.ApplicationSystems.WebApi.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);

ServicesSection(builder.Services);
var app = builder.Build();
UseSection(app);
app.Run();

void ServicesSection(IServiceCollection services)
{
    services.AddControllers().AddCoreJsonOptions().AddApiFluentValidateFilter();
    services.AddApiBehaivorConfigure();
    services.AddCoreFluentValidation<Program>();
    services.AddHttpContextAccessor();
    services.AddEndpointsApiExplorer();
    if (builder.Environment.IsDevelopment())
        services.AddCustomSwagger();

    services.AddCustomCors(builder.Configuration);
    services.AddRegisterDbContext(builder.Configuration);
    services.AddJwtAuthentication(builder.Configuration);

    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule<AutofacModule>());
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
}

void UseSection(WebApplication app)
{
    if (app.Environment.IsDevelopment())
        app.UseCustomSwagger();
    else
        app.UseHttpsRedirection();

    app.UseApiExceptionHandler();
    app.UseCors("AllowAll");
    app.UseAuthentication();
    app.MapControllers();
}

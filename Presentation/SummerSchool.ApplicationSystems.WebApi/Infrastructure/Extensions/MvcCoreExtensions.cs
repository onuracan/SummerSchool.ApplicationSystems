using Serilog;
using SummerSchool.ApplicationSystems.WebApi.Infrastructure.Filters;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace SummerSchool.ApplicationSystems.WebApi.Infrastructure.Extensions;

public static class MvcCoreExtensions
{
    public static IMvcBuilder AddCoreJsonOptions(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            x.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic);
            x.JsonSerializerOptions.WriteIndented = true;
            x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });
        return builder;
    }
    public static IMvcBuilder AddApiFluentValidateFilter(this IMvcBuilder builder)
    {
        builder.AddMvcOptions(x => x.Filters.Add<ApiFluentValidateFilterAttribute>());
        return builder;
    }

    public static WebApplicationBuilder AddSeriLog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((hostingContext, services, configuration) =>
        {
            configuration.ReadFrom.Configuration(hostingContext.Configuration)
                         .ReadFrom.Services(services)
                         .Enrich.FromLogContext();
        });
        return builder;
    }
}

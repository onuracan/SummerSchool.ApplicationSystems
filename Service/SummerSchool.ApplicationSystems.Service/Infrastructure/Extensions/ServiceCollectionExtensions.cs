using Microsoft.Extensions.DependencyInjection;
using SummerSchool.ApplicationSystems.Service.Infrastructure.Extensions;
using SummerSchool.ApplicationSystems.Service.Mappings.OtpVerification;

namespace SummerSchool.ApplicationSystems.Service.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(OtpVerificationProfile).Assembly);
        });

        return services;
    }
}

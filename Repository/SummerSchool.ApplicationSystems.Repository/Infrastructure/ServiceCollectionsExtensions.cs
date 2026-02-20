using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SummerSchool.ApplicationSystems.Repository.Context;
using System.Reflection;

namespace SummerSchool.ApplicationSystems.Repository.Infrastructure;

public static class ServiceCollectionsExtensions
{
    public static void AddRegisterDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MainDbContext>(x =>
        {
            x.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), opt =>
            {
                opt.MigrationsAssembly(Assembly.GetAssembly(typeof(MainDbContext)).GetName().Name);
            });
            x.UseLazyLoadingProxies(false);
        });
    }
}

using Autofac;
using SummerSchool.ApplicationSystems.Core.Repositories.Base;
using SummerSchool.ApplicationSystems.Core.Services.Base;
using SummerSchool.ApplicationSystems.Repository.Context;
using SummerSchool.ApplicationSystems.Repository.Repositories.Base;
using SummerSchool.ApplicationSystems.Service.Services.Base;
using System.Reflection;
using Module = Autofac.Module;

namespace SummerSchool.ApplicationSystems.WebApi.Infrastructure.Modules;

public class AutofacModule : Module
{
    protected override void Load(Autofac.ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>)).InstancePerLifetimeScope();

        var apiAssembly = Assembly.GetExecutingAssembly();
        var repoAssembly = Assembly.GetAssembly(typeof(MainDbContext));
        var serviceAssembly = Assembly.GetAssembly(typeof(BaseService<>));

        builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
            .Where(x => x.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
            .Where(x => x.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        base.Load(builder);
    }
}

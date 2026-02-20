using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SummerSchool.ApplicationSystems.Repository.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
{
    public MainDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationManager();
        configuration.SetBasePath(Directory.GetCurrentDirectory());
        configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var builder = new DbContextOptionsBuilder<MainDbContext>();
        builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new MainDbContext(builder.Options);
    }
}

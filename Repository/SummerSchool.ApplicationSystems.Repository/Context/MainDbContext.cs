using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace SummerSchool.ApplicationSystems.Repository.Context;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
        this.ChangeTracker.AutoDetectChangesEnabled = false;
        this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public IDbContextTransaction CreateTransaction()
    {
        if (base.Database.CurrentTransaction == null)
        {
            return base.Database.BeginTransaction();
        }

        return base.Database.CurrentTransaction;
    }

    public async Task<IDbContextTransaction> CreateTransactionAsync()
    {
        return (base.Database.CurrentTransaction == null) ? (await base.Database.BeginTransactionAsync()) : base.Database.CurrentTransaction;
    }
}

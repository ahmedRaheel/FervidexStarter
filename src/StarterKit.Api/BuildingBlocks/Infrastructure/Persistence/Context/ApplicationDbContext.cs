using StarterKit.Api.BuildingBlocks.Domain.Entities;
using StarterKit.Api.BuildingBlocks.Domain.Interfaces;
using System.Reflection;

namespace StarterKit.Api.BuildingBlocks.Infrastructure.Persistence.Context;

public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInformation();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        ApplyAuditInformation();
        return base.SaveChanges();
    }

    private void ApplyAuditInformation()
    {
        foreach (var entry in ChangeTracker.Entries<IAuditable>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.SetCreated(TimeProvider.System);
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.SetUpdated(TimeProvider.System);
            }
        }
    }
}

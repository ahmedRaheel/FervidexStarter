using StarterKit.Api.BuildingBlocks.Domain.Entities;
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
}

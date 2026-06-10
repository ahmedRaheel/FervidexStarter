using Microsoft.EntityFrameworkCore;
using StarterKit.Api.BuildingBlocks.Domain.Entities;
namespace StarterKit.Api.Infrastructure.Persistence.Context;
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
  
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
public sealed class AuditLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? UserId { get; set; }
    public string Action { get; set; } = default!;
    public string EntityName { get; set; } = default!;
    public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
}

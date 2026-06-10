using StarterKit.Api.BuildingBlocks.Domain.Entities;
using StarterKit.Api.BuildingBlocks.Infrastructure.Persistence.Context;
using System.Reflection;

namespace StarterKit.Api.BuildingBlocks.Infrastructure.Persistence.Seed;

public static class ApplicationDbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();

        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var autoMigrate = configuration.GetValue("SeedData:AutoMigrate", false);
        if (autoMigrate)
        {
            await db.Database.MigrateAsync(cancellationToken);
        }

        if (await db.Products.AnyAsync(cancellationToken))
        {
            return;
        }

        var products = new[]
        {
            CreateProduct(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Starter API Template", 99.00m, "STARTER-API"),
            CreateProduct(Guid.Parse("22222222-2222-2222-2222-222222222222"), "Vertical Slice Template", 149.00m, "VERTICAL-SLICE"),
            CreateProduct(Guid.Parse("33333333-3333-3333-3333-333333333333"), "Clean Architecture Template", 199.00m, "CLEAN-ARCH")
        };

        db.Products.AddRange(products);
        await db.SaveChangesAsync(cancellationToken);
    }

    private static Product CreateProduct(Guid id, string name, decimal price, string sku)
    {
        var product = new Product(name, price, sku);
        SetProtectedProperty(product, nameof(Product.Id), id);
        product.SetCreated(TimeProvider.System, "SeedData");
        return product;
    }

    private static void SetProtectedProperty<TValue>(object target, string propertyName, TValue value)
    {
        var property = target.GetType().GetProperty(
            propertyName,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        property?.SetValue(target, value);
    }
}

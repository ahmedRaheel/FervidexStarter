using Microsoft.EntityFrameworkCore;
using StarterKit.Api.BuildingBlocks.Domain.Entities;
using StarterKit.Api.Infrastructure.Persistence.Context;
namespace StarterKit.Api.Infrastructure.Persistence.Seed;
public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider sp)
    {
        var db=sp.GetRequiredService<AppDbContext>(); 
       
        if(!await db.Products.AnyAsync()) 
            db.Products.AddRange(new Product("Enterprise API Starter",149,"API-STARTER"), 
                new Product("SaaS Boilerplate",499,"SAAS-BOILER"));
        await db.SaveChangesAsync();
    }
}

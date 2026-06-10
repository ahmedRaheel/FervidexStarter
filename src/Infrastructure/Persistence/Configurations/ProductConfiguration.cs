using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarterKit.Api.BuildingBlocks.Domain.Entities;
namespace StarterKit.Api.Infrastructure.Persistence.Configurations;
public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> b)
    {
        b.ToTable("Products"); b.HasKey(x=>x.Id);
        b.Property(x=>x.Name).HasMaxLength(200).IsRequired();
        b.Property(x=>x.Sku).HasMaxLength(64).IsRequired(); b.HasIndex(x=>x.Sku).IsUnique();
        b.Property(x=>x.Price).HasColumnType("decimal(18,2)");
    }
}

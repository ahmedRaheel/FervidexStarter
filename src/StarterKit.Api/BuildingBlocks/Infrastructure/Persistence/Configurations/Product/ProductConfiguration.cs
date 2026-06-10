using StarterKit.Api.BuildingBlocks.Domain.Entities;

namespace StarterKit.Api.BuildingBlocks.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(Product.CreatedByMaxLength)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(Product.UpdatedByMaxLength);

        builder.Property(x => x.UpdatedAt);
    }
}

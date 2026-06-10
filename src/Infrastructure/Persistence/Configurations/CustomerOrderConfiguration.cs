using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarterKit.Api.BuildingBlocks.Domain.Entities;
namespace StarterKit.Api.Infrastructure.Persistence.Configurations;
public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>{ public void Configure(EntityTypeBuilder<Customer> b){ b.ToTable("Customers"); b.HasKey(x=>x.Id); b.Property(x=>x.Name).HasMaxLength(200); b.Property(x=>x.Email).HasMaxLength(256); } }
public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>{ public void Configure(EntityTypeBuilder<Order> b){ b.ToTable("Orders"); b.HasKey(x=>x.Id); b.Property(x=>x.TotalAmount).HasColumnType("decimal(18,2)"); b.Property(x=>x.Status).HasMaxLength(32); } }

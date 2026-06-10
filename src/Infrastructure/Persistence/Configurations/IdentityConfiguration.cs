using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarterKit.Api.Infrastructure.Identity.Entities;
namespace StarterKit.Api.Infrastructure.Persistence.Configurations;
public sealed class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> b){ b.ToTable("Users"); b.HasKey(x=>x.Id); b.HasIndex(x=>x.Email).IsUnique(); b.Property(x=>x.Email).HasMaxLength(256); b.Property(x=>x.Role).HasMaxLength(64); }
}
public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> b){ b.ToTable("RefreshTokens"); b.HasKey(x=>x.Id); b.Property(x=>x.Token).HasMaxLength(512); b.HasIndex(x=>x.Token).IsUnique(); }
}

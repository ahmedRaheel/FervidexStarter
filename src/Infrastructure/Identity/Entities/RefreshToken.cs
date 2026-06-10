using StarterKit.Api.BuildingBlocks.Domain.Base;
namespace StarterKit.Api.Infrastructure.Identity.Entities;
public sealed class RefreshToken : BaseEntity
{
    private RefreshToken() { }
    public RefreshToken(Guid userId, string token, DateTime expiresAtUtc)
    { UserId=userId; Token=token; ExpiresAtUtc=expiresAtUtc; }
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = default!;
    public DateTime ExpiresAtUtc { get; private set; }
    public DateTime? RevokedAtUtc { get; private set; }
    public bool IsActive => RevokedAtUtc is null && ExpiresAtUtc > DateTime.UtcNow;
    public void Revoke() => RevokedAtUtc = DateTime.UtcNow;
}

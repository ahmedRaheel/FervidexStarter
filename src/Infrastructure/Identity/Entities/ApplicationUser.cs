using StarterKit.Api.BuildingBlocks.Domain.Base;
namespace StarterKit.Api.Infrastructure.Identity.Entities;
public sealed class ApplicationUser : AuditableEntity
{
    private ApplicationUser() { }
    public ApplicationUser(string email, string passwordHash, string role)
    { Email=email; PasswordHash=passwordHash; Role=role; }
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string Role { get; private set; } = "User";
    public string Permissions { get; private set; } = "";
    public void SetPermissions(IEnumerable<string> permissions) => Permissions = string.Join(',', permissions);
}

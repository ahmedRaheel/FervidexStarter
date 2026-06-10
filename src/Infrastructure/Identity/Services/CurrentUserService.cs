using System.Security.Claims;
namespace StarterKit.Api.Infrastructure.Identity.Services;
public interface ICurrentUserService { Guid? UserId { get; } }
public sealed class CurrentUserService(IHttpContextAccessor accessor) : ICurrentUserService
{ public Guid? UserId => Guid.TryParse(accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null; }

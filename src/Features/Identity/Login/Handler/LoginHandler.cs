using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StarterKit.Api.BuildingBlocks.Security.JWT;
using StarterKit.Api.Features.Identity.Login.Command;
using StarterKit.Api.Infrastructure.Identity.Entities;
using StarterKit.Api.Infrastructure.Identity.Services;
using StarterKit.Api.Infrastructure.Persistence.Context;
namespace StarterKit.Api.Features.Identity.Login.Handler;
public sealed class LoginHandler(AppDbContext db, IPasswordHasher hasher, IJwtTokenService jwt, IOptions<JwtOptions> options) : IRequestHandler<LoginCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(LoginCommand r, CancellationToken ct)
    {
        var user=await db.Users.SingleOrDefaultAsync(x=>x.Email==r.Email,ct);
        if(user is null || !hasher.Verify(r.Password,user.PasswordHash)) throw new UnauthorizedAccessException("Invalid credentials");
        var refresh=jwt.CreateRefreshToken(); var expires=DateTime.UtcNow.AddDays(options.Value.RefreshTokenDays);
        db.RefreshTokens.Add(new RefreshToken(user.Id,refresh,expires)); await db.SaveChangesAsync(ct);
        return new AuthResponse(jwt.CreateAccessToken(user),refresh,expires);
    }
}

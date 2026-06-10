using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StarterKit.Api.BuildingBlocks.Security.JWT;
using StarterKit.Api.Features.Identity.Login.Command;
using StarterKit.Api.Infrastructure.Identity.Entities;
using StarterKit.Api.Infrastructure.Identity.Services;
using StarterKit.Api.Infrastructure.Persistence.Context;
namespace StarterKit.Api.Features.Identity.RefreshToken;
public sealed record RefreshTokenRequest(string RefreshToken);
public sealed record RefreshTokenCommand(string Token) : IRequest<AuthResponse>;
public sealed class RefreshTokenHandler(AppDbContext db,IJwtTokenService jwt,IOptions<JwtOptions> options) : IRequestHandler<RefreshTokenCommand,AuthResponse>
{
    public async Task<AuthResponse> Handle(RefreshTokenCommand r,CancellationToken ct){ var token=await db.RefreshTokens.SingleOrDefaultAsync(x=>x.Token==r.Token,ct); if(token is null || !token.IsActive) throw new UnauthorizedAccessException("Invalid refresh token"); token.Revoke(); var user=await db.Users.FindAsync([token.UserId],ct) ?? throw new UnauthorizedAccessException(); var newToken=jwt.CreateRefreshToken(); var exp=DateTime.UtcNow.AddDays(options.Value.RefreshTokenDays); db.RefreshTokens.Add(new RefreshToken(user.Id,newToken,exp)); await db.SaveChangesAsync(ct); return new AuthResponse(jwt.CreateAccessToken(user),newToken,exp); }
}
public static class RefreshTokenEndpoint{ public static IEndpointRouteBuilder MapRefreshToken(this IEndpointRouteBuilder app){ app.MapPost("/api/v1/auth/refresh", async (RefreshTokenRequest req,ISender sender,CancellationToken ct)=> Results.Ok(await sender.Send(new RefreshTokenCommand(req.RefreshToken),ct))).AllowAnonymous().WithTags("Identity"); return app; } }

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StarterKit.Api.BuildingBlocks.Security.JWT;
using StarterKit.Api.Infrastructure.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace StarterKit.Api.Infrastructure.Identity.Services;
public interface IJwtTokenService { string CreateAccessToken(ApplicationUser user); string CreateRefreshToken(); }
public sealed class JwtTokenService(IOptions<JwtOptions> options) : IJwtTokenService
{
    public string CreateAccessToken(ApplicationUser user)
    {
        var jwt=options.Value; var claims=new List<Claim>{ new(JwtRegisteredClaimNames.Sub,user.Id.ToString()), new(ClaimTypes.NameIdentifier,user.Id.ToString()), new(ClaimTypes.Email,user.Email), new(ClaimTypes.Role,user.Role)};
        claims.AddRange(user.Permissions.Split(',',StringSplitOptions.RemoveEmptyEntries).Select(p=>new Claim("permission",p)));
        var creds=new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret)),SecurityAlgorithms.HmacSha256);
        return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(jwt.Issuer,jwt.Audience,claims,expires:DateTime.UtcNow.AddMinutes(jwt.AccessTokenMinutes),signingCredentials:creds));
    }
    public string CreateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
}

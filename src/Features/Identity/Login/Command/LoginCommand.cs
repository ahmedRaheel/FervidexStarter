using MediatR;
namespace StarterKit.Api.Features.Identity.Login.Command;
public sealed record LoginRequest(string Email,string Password);
public sealed record AuthResponse(string AccessToken,string RefreshToken,DateTime ExpiresAtUtc);
public sealed record LoginCommand(string Email,string Password) : IRequest<AuthResponse>;

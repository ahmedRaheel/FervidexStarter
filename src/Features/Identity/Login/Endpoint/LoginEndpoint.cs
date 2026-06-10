using MediatR;
using StarterKit.Api.Features.Identity.Login.Command;
namespace StarterKit.Api.Features.Identity.Login.Endpoint;
public static class LoginEndpoint{ public static IEndpointRouteBuilder MapLogin(this IEndpointRouteBuilder app){ app.MapPost("/api/v1/auth/login", async (LoginRequest req, ISender sender, CancellationToken ct)=> Results.Ok(await sender.Send(new LoginCommand(req.Email,req.Password),ct))).AllowAnonymous().WithTags("Identity"); return app; } }

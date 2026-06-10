using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StarterKit.Api.Infrastructure.Identity.Entities;
using StarterKit.Api.Infrastructure.Identity.Services;
using StarterKit.Api.Infrastructure.Persistence.Context;
namespace StarterKit.Api.Features.Identity.Register;
public sealed record RegisterRequest(string Email,string Password);
public sealed record RegisterResponse(Guid UserId,string Email);
public sealed record RegisterCommand(string Email,string Password) : IRequest<RegisterResponse>;
public sealed class RegisterValidator : AbstractValidator<RegisterCommand>{ public RegisterValidator(){ RuleFor(x=>x.Email).EmailAddress(); RuleFor(x=>x.Password).MinimumLength(8); } }
public sealed class RegisterHandler(AppDbContext db, IPasswordHasher hasher) : IRequestHandler<RegisterCommand,RegisterResponse>
{
    public async Task<RegisterResponse> Handle(RegisterCommand r,CancellationToken ct){ if(await db.Users.AnyAsync(x=>x.Email==r.Email,ct)) throw new InvalidOperationException("Email already registered"); var u=new ApplicationUser(r.Email,hasher.Hash(r.Password),"User"); db.Users.Add(u); await db.SaveChangesAsync(ct); return new(u.Id,u.Email); }
}
public static class RegisterEndpoint{ public static IEndpointRouteBuilder MapRegister(this IEndpointRouteBuilder app){ app.MapPost("/api/v1/auth/register", async (RegisterRequest req, ISender sender, CancellationToken ct)=> Results.Ok(await sender.Send(new RegisterCommand(req.Email,req.Password),ct))).AllowAnonymous().WithTags("Identity"); return app; } }

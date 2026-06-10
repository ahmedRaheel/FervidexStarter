using FluentValidation;
using StarterKit.Api.Features.Identity.Login.Command;
namespace StarterKit.Api.Features.Identity.Login.Validator;
public sealed class LoginValidator : AbstractValidator<LoginCommand>{ public LoginValidator(){ RuleFor(x=>x.Email).EmailAddress(); RuleFor(x=>x.Password).NotEmpty(); } }

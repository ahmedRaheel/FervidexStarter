using Microsoft.AspNetCore.Authorization;
namespace StarterKit.Api.BuildingBlocks.Security.Authorization;
public sealed record PermissionRequirement(string Permission) : IAuthorizationRequirement;
public sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User.IsInRole(Roles.Admin) || context.User.HasClaim("permission", requirement.Permission)) context.Succeed(requirement);
        return Task.CompletedTask;
    }
}

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using StarterKit.Api.BuildingBlocks.Infrastructure.Persistence.Context;

namespace StarterKit.Api.Middleware.Observability.HealthChecks;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddStarterKitHealthChecks(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var redisConnection =
            configuration.GetConnectionString("Redis");

        var checks = services
            .AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>(
                name: "database",
                tags: ["ready"]);

        if (!string.IsNullOrWhiteSpace(redisConnection))
        {
            checks.AddRedis(
                redisConnection,
                name: "redis",
                tags: ["ready"]);
        }

        return services;
    }

    public static IEndpointRouteBuilder MapStarterKitHealthChecks(
        this IEndpointRouteBuilder app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = HealthCheckResponseWriter.WriteResponseAsync
        });

        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = _ => false,
            ResponseWriter = HealthCheckResponseWriter.WriteResponseAsync
        });

        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("ready"),
            ResponseWriter = HealthCheckResponseWriter.WriteResponseAsync
        });

        return app;
    }
}

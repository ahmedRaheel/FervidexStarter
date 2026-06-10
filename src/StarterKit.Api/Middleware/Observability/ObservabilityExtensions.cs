using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using StarterKit.Api.BuildingBlocks.Infrastructure.Persistence.Context;

namespace StarterKit.Api.Middleware.Observability;

public static class ObservabilityExtensions
{
    public static IServiceCollection AddStarterKitObservability(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var serviceName = configuration.GetValue("OpenTelemetry:ServiceName", "FervidexStarter.Api");

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>(name: "database");

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation())
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation());

        return services;
    }

    public static WebApplication MapStarterKitHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = _ => false
        });

        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = check => check.Name == "database"
        });

        app.MapHealthChecks("/health");

        return app;
    }
}

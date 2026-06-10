using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using StarterKit.Api.BuildingBlocks.Infrastructure.Persistence.Context;
using StarterKit.Api.Middleware.Observability.HealthChecks;
using StarterKit.Api.Middleware.Observability.Metrics;
using StarterKit.Api.Middleware.Observability.OpenTelemetry;
using StarterKit.Api.Middleware.Observability.Tracing;

namespace StarterKit.Api.Middleware.Observability;

public static class ObservabilityExtensions
{
    public static IServiceCollection AddStarterKitObservability(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddStarterKitHealthChecks(configuration);
        services.AddStarterKitOpenTelemetry(configuration);
        services.AddStarterKitMetrics();
        services.AddStarterKitTracing();

        return services;
    }

    public static IEndpointRouteBuilder MapStarterKitObservability(
        this IEndpointRouteBuilder app)
    {
        app.MapStarterKitHealthChecks();

        return app;
    }
}
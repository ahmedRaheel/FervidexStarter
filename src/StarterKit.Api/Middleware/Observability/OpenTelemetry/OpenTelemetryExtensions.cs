using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace StarterKit.Api.Middleware.Observability.OpenTelemetry;

public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddStarterKitOpenTelemetry(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var serviceName =
            configuration["OpenTelemetry:ServiceName"]
            ?? "StarterKit.Api";

        services.AddOpenTelemetry()
            .ConfigureResource(resource =>
                resource.AddService(serviceName))
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation();

                tracing.AddOtlpExporter();
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation();

                metrics.AddOtlpExporter();
            });

        return services;
    }
}

using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace StarterKit.Api.Middleware.Observability.HealthChecks;

public static class HealthCheckResponseWriter
{
    public static async Task WriteResponseAsync(
        HttpContext context,
        HealthReport report)
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            status = report.Status.ToString(),
            totalDuration = report.TotalDuration.TotalMilliseconds,
            checks = report.Entries.Select(x => new
            {
                name = x.Key,
                status = x.Value.Status.ToString(),
                description = x.Value.Description,
                duration = x.Value.Duration.TotalMilliseconds,
                error = x.Value.Exception?.Message
            })
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}

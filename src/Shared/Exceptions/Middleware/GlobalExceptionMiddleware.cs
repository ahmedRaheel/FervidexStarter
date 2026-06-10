using FluentValidation; using System.Net;
namespace StarterKit.Api.Shared.Exceptions.Middleware;
public sealed class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try { await next(context); }
        catch(Exception ex){ logger.LogError(ex,"Unhandled exception"); context.Response.ContentType="application/json"; context.Response.StatusCode=ex switch { ValidationException => 400, UnauthorizedAccessException => 401, KeyNotFoundException => 404, _ => 500}; await context.Response.WriteAsJsonAsync(new { error=ex.Message, status=context.Response.StatusCode }); }
    }
}

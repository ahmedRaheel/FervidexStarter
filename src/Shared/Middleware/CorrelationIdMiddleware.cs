namespace StarterKit.Api.Shared.Middleware;
public sealed class CorrelationIdMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext ctx){ var id=ctx.Request.Headers.TryGetValue("X-Correlation-Id",out var v)? v.ToString():Guid.NewGuid().ToString(); ctx.Response.Headers["X-Correlation-Id"]=id; using var scope=ctx.RequestServices.GetRequiredService<ILogger<CorrelationIdMiddleware>>().BeginScope(new Dictionary<string,object>{{"CorrelationId",id}}); await next(ctx); }
}

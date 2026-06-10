using Asp.Versioning;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using StarterKit.Api.BuildingBlocks.Application.Behaviors;
using StarterKit.Api.BuildingBlocks.Caching.Redis;
using StarterKit.Api.BuildingBlocks.Logging.Serilog;
using StarterKit.Api.Infrastructure.Persistence.Context;
using StarterKit.Api.Infrastructure.Persistence.Seed;
using StarterKit.Api.Shared.Exceptions.Middleware;
using StarterKit.Api.Shared.Middleware;
using StarterKit.Api.StarterKit.Api.Extensions;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, cfg) => cfg.ConfigureSerilog(ctx.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

var provider = builder.Configuration.GetValue("Database:Provider", "SqlServer");
var connectionString = builder.Configuration.GetConnectionString(provider) ?? builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (provider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase)) options.UseNpgsql(connectionString);
    else options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IDbConnectionFactory>(_ => new DbConnectionFactory(provider, connectionString));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));


builder.Services.AddHttpContextAccessor();


builder.Services.AddAuthorization();



builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();
builder.Services.AddOutputCache();
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("global", limiter =>
    {
        limiter.Window = TimeSpan.FromMinutes(1);
        limiter.PermitLimit = 100;
        limiter.QueueLimit = 10;
        limiter.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});
builder.Services.AddHttpClient("resilient");
builder.Services.AddHealthChecks();
builder.Services.AddOpenTelemetry().ConfigureResource(r => r.AddService("FStarter.Api"))
    .WithTracing(t => t.AddAspNetCoreInstrumentation().AddHttpClientInstrumentation())
    .WithMetrics(m => m.AddAspNetCoreInstrumentation().AddHttpClientInstrumentation());

var app = builder.Build();

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseSerilogRequestLogging();
app.UseSwaggerDocumentation();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.UseOutputCache();
app.MapHealthChecks("/health");
app.MapApiEndpoints();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
    await DatabaseSeeder.SeedAsync(scope.ServiceProvider);
}

app.Run();

public partial class Program { }

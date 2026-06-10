using Serilog;
namespace StarterKit.Api.BuildingBlocks.Logging.Serilog;
public static class SerilogConfiguration
{
    /// <summary>
    /// Configuring Serilog with settings 
    /// from appsettings.json and enrichers for log context, 
    /// machine name, and environment name.
    /// </summary>
    /// <param name="cfg"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static LoggerConfiguration ConfigureSerilog(this LoggerConfiguration cfg, IConfiguration configuration)
        => cfg.ReadFrom.Configuration(configuration)
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithEnvironmentName();
}
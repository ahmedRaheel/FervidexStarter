using Polly;
using Polly.Extensions.Http;
namespace StarterKit.Api.BuildingBlocks.Infrastructure.External;
public static class PollyPolicies 
{
    public static IAsyncPolicy<HttpResponseMessage> RetryPolicy()
         =>HttpPolicyExtensions.HandleTransientHttpError()
        .WaitAndRetryAsync(3, r=>TimeSpan.FromSeconds(r));
}

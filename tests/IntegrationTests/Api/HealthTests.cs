using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
namespace IntegrationTests.Api;
public sealed class HealthTests : IClassFixture<WebApplicationFactory<Program>>
{ 
    private readonly WebApplicationFactory<Program> _factory;
    public HealthTests(WebApplicationFactory<Program> factory)=>_factory=factory;
    [Fact(Skip="Requires database container or local db")] 
    public async Task Health_should_return_success()
    {
        var client=_factory.CreateClient(); 
        var res=await client.GetAsync("/health");
        res.EnsureSuccessStatusCode();
    }
}

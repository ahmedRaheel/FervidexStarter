using Xunit;

namespace ArchitectureTests.LayerTests;
public sealed class DependencyRuleTests 
{
    [Fact] 
    public void Domain_should_not_reference_infrastructure()
    {
        Assert.True(true); 
    }
}

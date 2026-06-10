using FluentAssertions;
using StarterKit.Api.BuildingBlocks.Domain.Entities;
using Xunit;
namespace UnitTests.Domain;
public sealed class ProductTests 
{
    [Fact] 
    public void Create_product_should_set_values()
    {
        var p=new Product("API",10,"SKU");
        p.Name.Should().Be("API"); 
        p.Price.Should().Be(10);
    }
}

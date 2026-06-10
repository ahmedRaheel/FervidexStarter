using StarterKit.Api.BuildingBlocks.Domain.Base;
namespace StarterKit.Api.BuildingBlocks.Domain.Entities;
public sealed class Product : AuditableEntity
{
    private Product() { }
    public Product(string name, decimal price, string sku)
    {
        Name = name; Price = price; Sku = sku;
    }
    public string Name { get; private set; } = default!;
    public string Sku { get; private set; } = default!;
    public decimal Price { get; private set; }
    public void Update(string name, decimal price, string sku) { Name=name; Price=price; Sku=sku; MarkUpdated(); }
}

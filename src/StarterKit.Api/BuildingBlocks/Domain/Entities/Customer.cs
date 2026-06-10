using StarterKit.Api.BuildingBlocks.Domain.Base;
namespace StarterKit.Api.BuildingBlocks.Domain.Entities;
public sealed class Customer : AuditableEntity
{
    private Customer() { }
    public Customer(string name, string email) { Name=name; Email=email; }
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public void Update(string name, string email) { Name=name; Email=email; MarkUpdated(); }
}

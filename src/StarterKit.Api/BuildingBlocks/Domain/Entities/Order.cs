using StarterKit.Api.BuildingBlocks.Domain.Base;
namespace StarterKit.Api.BuildingBlocks.Domain.Entities;
public sealed class Order : AuditableEntity
{
    private Order() { }
    public Order(Guid customerId, decimal totalAmount) { CustomerId=customerId; TotalAmount=totalAmount; Status="Pending"; }
    public Guid CustomerId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string Status { get; private set; } = "Pending";
    public void UpdateStatus(string status) { Status=status; MarkUpdated(); }
    public void Cancel() { Status="Cancelled"; MarkUpdated(); }
}

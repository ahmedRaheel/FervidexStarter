namespace StarterKit.Api.BuildingBlocks.Domain.Base;
/// <summary>
///  Represents the base class for all entities in the domain. 
///  It provides common properties such as Id, CreatedAtUtc, and UpdatedAtUtc.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAtUtc { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; protected set; }
    public void MarkUpdated() => UpdatedAtUtc = DateTime.UtcNow;
}
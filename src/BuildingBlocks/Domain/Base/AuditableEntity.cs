namespace StarterKit.Api.BuildingBlocks.Domain.Base;

/// <summary>
/// Represents an entity that can be audited, meaning it keeps track of who created and last updated the entity,
/// as well as whether the entity has been marked as deleted.
/// This is useful for maintaining a history of changes and ensuring data integrity.
/// </summary>
public abstract class AuditableEntity : BaseEntity
{
    public Guid? CreatedBy { get; protected set; }
    public Guid? UpdatedBy { get; protected set; }
    public bool IsDeleted { get; protected set; }
    public void Delete() => IsDeleted = true;
}

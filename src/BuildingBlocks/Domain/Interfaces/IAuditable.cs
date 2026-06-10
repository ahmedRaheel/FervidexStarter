namespace StarterKit.Api.BuildingBlocks.Domain.Interfaces;

/// <summary>
/// Represents an entity that can be audited, meaning it keeps track of who created and last updated the entity,
/// as well as whether the entity has been marked as deleted.
/// This is useful for maintaining a history of changes and ensuring data integrity.
/// </summary>

public interface IAuditable
{
    DateTimeOffset CreatedAt { get; }
    string CreatedBy { get; }
    DateTimeOffset? UpdatedAt { get; }
    string? UpdatedBy { get; }

    void SetCreated(TimeProvider timeProvider, string? createdBy = null);

    void SetUpdated(TimeProvider timeProvider, string? updatedBy = null);
}


using StarterKit.Api.BuildingBlocks.Domain.Interfaces;
using StarterKit.Api.BuildingBlocks.Domain.Base;

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

 
public abstract class Entity<TId> : BaseEntity
{
    public new TId Id { get; protected set; }
}





/// <summary>
/// Tracks creation and modification of objects.
/// </summary>
public abstract class AuditableEntity<TId> : Entity<TId>, IAuditable
{
    public const int CreatedByMaxLength = 128;
    public const int UpdatedByMaxLength = 128;

    private const string SystemUser = "System";

    public DateTimeOffset CreatedAt { get; private set; }

    public string CreatedBy
    {
        get;
        private set
        {
            //ThrowIfNullOrWhiteSpace(value, nameof(CreatedBy));
            //ThrowIfGreaterThan(value.Length, CreatedByMaxLength, nameof(CreatedBy));
            field = value;
        }
    } = null!;

    public DateTimeOffset? UpdatedAt { get; private set; }

    public string? UpdatedBy
    {
        get;
        private set
        {
            //ThrowIfNullOrWhiteSpace(value, nameof(UpdatedBy));
            //ThrowIfGreaterThan(value.Length, UpdatedByMaxLength, nameof(UpdatedBy));
            field = value;
        }
    }

    public void SetCreated(TimeProvider timeProvider, string? createdBy)
    {
        CreatedAt = timeProvider.GetUtcNow();
        CreatedBy = createdBy ?? SystemUser;
    }

    public void SetUpdated(TimeProvider timeProvider, string? updatedBy)
    {
        UpdatedAt = timeProvider.GetUtcNow();
        UpdatedBy = updatedBy ?? SystemUser;
    }
}
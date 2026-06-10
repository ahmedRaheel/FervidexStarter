using System.Data.Common;

/// <summary>
/// Factory for creating database connections.
/// </summary>
public interface IDbConnectionFactory
{
    DbConnection CreateConnection();

    Task<DbConnection> CreateOpenConnectionAsync(
        CancellationToken cancellationToken = default);
}
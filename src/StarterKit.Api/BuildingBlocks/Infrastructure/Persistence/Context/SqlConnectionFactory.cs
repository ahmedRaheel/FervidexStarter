using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace StarterKit.Api.BuildingBlocks.Infrastructure.Persistence.Context;

public sealed class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Database connection string not configured.");
    }

    public DbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<DbConnection> CreateOpenConnectionAsync(
        CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        return connection;
    }
}
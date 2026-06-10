using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;
namespace StarterKit.Api.Infrastructure.Persistence.Context;
public interface IDbConnectionFactory { IDbConnection CreateConnection(); }
public sealed class DbConnectionFactory(string provider, string connectionString) : IDbConnectionFactory
{
    public IDbConnection CreateConnection() => provider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase)
        ? new NpgsqlConnection(connectionString)
        : new SqlConnection(connectionString);
}

using Npgsql;

namespace Recipes.Api.Data.Dapper;

public sealed class DapperConnectionFactory
{
    private readonly string _connectionString;

    public DapperConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("PostgresDb")
                            ?? throw new InvalidOperationException(
                                "DefaultConnection is not configured.");
    }

    public NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
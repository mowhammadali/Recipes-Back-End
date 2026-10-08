using Dapper;
using Recipes.Api.Data.Dapper;
using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Repositories;

public class DapperMealTypeRepository : IDapperMealTypeRepository
{
    private readonly DapperConnectionFactory _dapperConnectionFactory;

    public DapperMealTypeRepository(DapperConnectionFactory dapperConnectionFactory)
    {
        _dapperConnectionFactory = dapperConnectionFactory;
    }

    public async Task<List<MealType>> GetAllAsync()
    {
        using var connection = _dapperConnectionFactory.CreateConnection();

        const string sql = """
                           select
                               *
                           from "MealTypes";
                           """;

        var response = await connection.QueryAsync<MealType>(sql);

        return response.ToList();
    }
}
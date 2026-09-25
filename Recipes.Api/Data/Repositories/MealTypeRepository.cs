using Microsoft.EntityFrameworkCore;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Repositories;

public class MealTypeRepository : IMealTypeRepository
{
    private readonly AppDbContext _dbContext;

    public MealTypeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<MealType>> GetAllAsync()
    {
        var mealTypes = await _dbContext.MealTypes.ToListAsync();

        return mealTypes;
    }
}
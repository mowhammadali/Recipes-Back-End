using Microsoft.EntityFrameworkCore;
using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Models.Entities;
using Recipes.Api.Models.Queries;

namespace Recipes.Api.Data.Repositories;

public sealed class RecipeRepository : IRecipeRepository
{
    private readonly AppDbContext _dbContext;

    public RecipeRepository(AppDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Recipe?> GetByIdAsync(Guid id)
    {
        var recipe = await _dbContext.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.Instructions)
            .Include(r => r.MealTypes)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);

        return recipe;
    }

    public async Task<List<MealType>> GetMealTypesByIdsAsync(List<Guid> ids)
    {
        var mealTypes = await _dbContext.MealTypes.Where(m => ids.Contains(m.Id)).ToListAsync();

        return mealTypes;
    }

    public IQueryable<Recipe> Query()
    {
        return _dbContext.Recipes.AsNoTracking()
            .Include(r => r.Ingredients)
            .Include(r => r.Instructions)
            .Include(r => r.MealTypes)
            .Include(r => r.User);
    }

    public async Task AddAsync(Recipe recipe)
    {
        await _dbContext.Recipes.AddAsync(recipe);
    }

    public void Update(Recipe recipe)
    {
        throw new NotImplementedException();
    }

    public void Delete(Recipe recipe)
    {
        _dbContext.Recipes.Remove(recipe);
    }

    public async Task<List<RecipeCountByMealType>> GetRecipeCountByMealTypeAsync()
    {
        var query = await _dbContext.Recipes.SelectMany(x => x.MealTypes)
            .GroupBy(x => new { Id = x.Id, Name = x.Name })
            .Select(x => new RecipeCountByMealType(x.Key.Id, x.Key.Name, x.Count())).ToListAsync();

        return query;
    }
}
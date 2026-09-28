using Microsoft.EntityFrameworkCore;
using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Repositories;

public sealed class RecipeRepository : IRecipeRepository
{
    private readonly AppDbContext _dbContext;

    public RecipeRepository(AppDbContext context)
    {
        _dbContext = context;
    }

    public Task<Recipe?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Recipe> Query()
    {
        return _dbContext.Recipes.AsNoTracking()
            .Include(r => r.Ingredients)
            .Include(r => r.Instructions)
            .Include(r => r.MealTypes)
            .Include(r => r.User);
    }

    public Task<List<Recipe>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Recipe recipe)
    {
        throw new NotImplementedException();
    }

    public void Update(Recipe recipe)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
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
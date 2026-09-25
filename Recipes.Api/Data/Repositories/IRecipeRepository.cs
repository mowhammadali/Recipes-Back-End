using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Repositories;

public interface IRecipeRepository
{
    Task<Recipe?> GetByIdAsync(Guid id);
    Task<List<Recipe>> GetAllAsync();
    Task AddAsync(Recipe recipe);
    void Update(Recipe recipe);
    void Delete(Guid id);
}
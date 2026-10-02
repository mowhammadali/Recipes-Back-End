using Recipes.Api.Models.Entities;
using Recipes.Api.Models.Queries;

namespace Recipes.Api.Data.Repositories.Interfaces;

public interface IRecipeRepository
{
    Task<Recipe?> GetByIdAsync(Guid id);
    Task<List<MealType>> GetMealTypesByIdsAsync(List<Guid> ids);
    IQueryable<Recipe> Query();
    Task AddAsync(Recipe recipe);
    void Update(Recipe recipe);
    void Delete(Recipe recipe);
    Task<List<RecipeCountByMealType>> GetRecipeCountByMealTypeAsync();
}
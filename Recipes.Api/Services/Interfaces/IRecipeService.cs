using Recipes.Api.Models.DTOs.Common;
using Recipes.Api.Models.DTOs.Recipes;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Services.Interfaces;

public interface IRecipeService
{
    Task<PagedResponse<RecipeResponse>> GetAllAsync(RecipeQueryParameters queryParameters);
    Task<RecipeResponse> GetByIdAsync(Guid id);
    Task<RecipeResponse> AddAsync(Guid userId, CreateRecipeRequest createRecipeRequest);
    Task DeleteAsync(Guid recipeId , Guid userId);
}
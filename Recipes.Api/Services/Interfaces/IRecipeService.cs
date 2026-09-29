using Recipes.Api.Models.DTOs.Common;
using Recipes.Api.Models.DTOs.Recipes;

namespace Recipes.Api.Services.Interfaces;

public interface IRecipeService
{
    Task<PagedResponse<RecipeResponse>> GetAllAsync(RecipeQueryParameters queryParameters);
    Task<RecipeResponse> GetByIdAsync(Guid id);
}
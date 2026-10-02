using Recipes.Api.Models.DTOs.Favorites;

namespace Recipes.Api.Services.Interfaces;

public interface IFavoriteService
{
    Task<List<FavoriteResponse>> GetAllAsync(Guid userId);
    Task AddFavoriteAsync(Guid userId, Guid recipeId);
}
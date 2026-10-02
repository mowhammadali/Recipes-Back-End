using Recipes.Api.Models.DTOs.Favorites;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Services.Interfaces;

public interface IFavoriteService
{
    Task<List<FavoriteResponse>> GetAllAsync(Guid userId);
    Task AddFavoriteAsync(Guid userId, Guid recipeId);
    Task RemoveFavoriteAsync(Guid userId, Guid favoriteId);
}
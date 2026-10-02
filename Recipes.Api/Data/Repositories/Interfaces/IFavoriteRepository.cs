using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Repositories.Interfaces;

public interface IFavoriteRepository
{
    Task<List<Favorite>> GetAllByUserIdAsync(Guid userId);
    Task<Favorite?> GetFavoriteAsync(Guid favoriteId);
    Task AddAsync(Favorite favorite);
    void Delete(Favorite favorite);
}
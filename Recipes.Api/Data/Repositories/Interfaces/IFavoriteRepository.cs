using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Repositories.Interfaces;

public interface IFavoriteRepository
{
    Task<List<Favorite>> GetAllByUserIdAsync(Guid userId);
    Task AddAsync(Favorite favorite);
    void Delete(Guid id);
}
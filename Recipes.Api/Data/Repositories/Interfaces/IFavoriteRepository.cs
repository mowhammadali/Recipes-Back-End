using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Repositories.Interfaces;

public interface IFavoriteRepository
{
    Task AddAsync(Favorite favorite);
    Task<Favorite?> GetByIdAsync(Guid id);
    void Delete(Guid id);
    Task<List<Favorite>> GetAllByUserIdAsync(Guid userId);
}
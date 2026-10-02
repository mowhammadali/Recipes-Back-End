using Recipes.Api.Models.Entities;

namespace Recipes.Api.Services.Interfaces;

public interface IFavoriteService
{
    Task<List<Favorite>> GetAllAsync(Guid userId);
}
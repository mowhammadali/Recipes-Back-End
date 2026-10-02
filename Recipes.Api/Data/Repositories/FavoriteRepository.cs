using Microsoft.EntityFrameworkCore;
using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Repositories;

public sealed class FavoriteRepository : IFavoriteRepository
{
    private readonly AppDbContext _dbContext;

    public FavoriteRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Favorite?> GetFavoriteAsync(Guid favoriteId)
    {
        return await _dbContext.Favorites.FirstOrDefaultAsync(x => x.Id == favoriteId);
    }

    public async Task<bool> CheckExistFavoriteByRecipeIdAsync(Guid userId, Guid recipeId)
    {
        return await _dbContext.Favorites.AnyAsync(x => x.RecipeId == recipeId && x.UserId == userId);
    }

    public async Task AddAsync(Favorite favorite)
    {
        await _dbContext.Favorites.AddAsync(favorite);
    }

    public void Delete(Favorite favorite)
    {
        _dbContext.Favorites.Remove(favorite);
    }

    public async Task<List<Favorite>> GetAllByUserIdAsync(Guid userId)
    {
        var favorites = await _dbContext.Favorites.Include(x => x.Recipe)
            .Where(x => x.UserId == userId).ToListAsync();

        return favorites;
    }
}
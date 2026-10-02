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

    public Task AddAsync(Favorite favorite)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Favorite>> GetAllByUserIdAsync(Guid userId)
    {
        var favorites = await _dbContext.Favorites.Include(x => x.Recipe)
            .Where(x => x.UserId == userId).ToListAsync();

        return favorites;
    }
}
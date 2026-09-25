using Recipes.Api.Data.Repositories.Interfaces;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly AppDbContext _dbContext;

    public UserProfileRepository(AppDbContext context)
    {
        _dbContext = context;
    }

    public Task<UserProfile?> GetByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public void Update(UserProfile profile)
    {
        throw new NotImplementedException();
    }
}
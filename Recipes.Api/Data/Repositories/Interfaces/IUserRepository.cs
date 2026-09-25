using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    void Update(User user);
}
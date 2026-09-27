using Recipes.Api.Models.DTOs.Users;

namespace Recipes.Api.Services.Interfaces;

public interface IUserService
{
    Task<List<UserResponse>> GetAllAsync();
    Task<UserResponse> GetByIdAsync(Guid userId);
    Task DeleteAsync(Guid userId);
}
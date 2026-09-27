using Recipes.Api.Models.DTOs.Users;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Services.Interfaces;

public interface IUserService
{
    Task<List<UserResponse>> GetAllAsync();
    Task<UserResponse> GetByIdAsync(Guid userId);
    Task DeleteAsync(Guid userId);
    Task UpdateByAdminAsync(Guid userId, AdminUpdateUserRequest user);
}
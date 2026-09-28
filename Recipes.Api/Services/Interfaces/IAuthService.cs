using Recipes.Api.Models.DTOs.Auth;
using Recipes.Api.Models.DTOs.Users;

namespace Recipes.Api.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest);
    Task<AuthResponse> LoginAsync(LoginRequest loginRequest);
    Task<UserResponse> GetMeAsync(Guid userId);
    Task ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);
}
using Recipes.Api.Models.DTOs.Auth;

namespace Recipes.Api.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest);
}
using Recipes.Api.Models.DTOs.Auth;

namespace Recipes.Api.Services.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterRequest registerRequest);
}
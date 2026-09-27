using Recipes.Api.Models.DTOs.Auth;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Services.Interfaces;

public interface IJwtService
{
    TokenGenerateResponse GenerateToken(User user);
}
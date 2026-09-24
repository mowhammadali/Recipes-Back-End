namespace Recipes.Api.Models.DTOs.Auth;

public sealed record LoginRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
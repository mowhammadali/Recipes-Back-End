namespace Recipes.Api.Models.DTOs.Auth;

public sealed record AuthResponse
{
    public string AccessToken { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
};
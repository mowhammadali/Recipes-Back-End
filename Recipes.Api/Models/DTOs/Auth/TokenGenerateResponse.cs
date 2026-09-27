namespace Recipes.Api.Models.DTOs.Auth;

public sealed record TokenGenerateResponse
{
    public string AccessToken { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
}
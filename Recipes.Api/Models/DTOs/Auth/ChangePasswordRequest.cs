namespace Recipes.Api.Models.DTOs.Auth;

public sealed record ChangePasswordRequest
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
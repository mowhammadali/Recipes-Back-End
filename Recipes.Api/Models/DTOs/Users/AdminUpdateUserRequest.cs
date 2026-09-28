using Recipes.Api.Models.Enums;

namespace Recipes.Api.Models.DTOs.Users;

public sealed record AdminUpdateUserRequest
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Password { get; set; }
    public UserRole Role { get; set; }
};
using Recipes.Api.Models.DTOs.UsersProfile;
using Recipes.Api.Models.Enums;

namespace Recipes.Api.Models.DTOs.Users;

public sealed record UserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; }
    public UserProfileResponse UserProfileResponse { get; set; } = null!;
}
using Recipes.Api.Models.DTOs.UsersProfile;

namespace Recipes.Api.Models.DTOs.Users;

public sealed record UserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserProfileResponse UserProfileResponse { get; set; } = null!;
}
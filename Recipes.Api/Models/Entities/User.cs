using Recipes.Api.Models.Enums;

namespace Recipes.Api.Models.Entities;

public sealed class User
{
    public Guid Id { get; init; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
    public UserRole Role { get; set; }
    public UserProfile UserProfile { get; set; } = null!;
    public ICollection<Recipe> Recipes { get; init; } = [];
}
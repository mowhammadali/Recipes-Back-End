namespace Recipes.Api.Models.Entities;

public sealed class UserProfile
{
    public Guid Id { get; init; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Bio { get; set; }
    public Guid UserId { get; init; }
    public User User { get; init; } = null!;
}
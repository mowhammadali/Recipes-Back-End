namespace Recipes.Api.Models.Entities;

public sealed class User
{
    public Guid Id { get; init; }
    public string Username { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string PasswordHash { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
}
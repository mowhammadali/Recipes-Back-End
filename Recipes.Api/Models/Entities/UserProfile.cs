namespace Recipes.Api.Models.Entities;

public sealed class UserProfile
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Bio { get; init; } = null!;
    public Guid UserId { get; init; }
}
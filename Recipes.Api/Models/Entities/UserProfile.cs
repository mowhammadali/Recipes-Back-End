namespace Recipes.Api.Models.Entities;

public sealed class UserProfile
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Bio { get; init; }
    public Guid UserId { get; init; }
}
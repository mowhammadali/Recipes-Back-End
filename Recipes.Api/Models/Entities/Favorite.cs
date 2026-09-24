namespace Recipes.Api.Models.Entities;

public sealed class Favorite
{
    public Guid Id { get; init; }
    public Guid RecipeId { get; init; }
    public Guid UserId { get; init; }
    public Recipe Recipe { get; init; } = null!;
    public User User { get; init; } = null!;
}
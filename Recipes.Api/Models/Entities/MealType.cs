namespace Recipes.Api.Models.Entities;

public sealed class MealType
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public ICollection<Recipe> Recipes { get; init; } = [];
}
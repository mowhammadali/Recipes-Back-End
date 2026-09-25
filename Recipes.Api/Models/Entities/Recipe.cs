using Recipes.Api.Models.Enums;
using Recipes.Api.Models.ValueObjects;

namespace Recipes.Api.Models.Entities;

public sealed class Recipe
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public int PrepTimeMinutes { get; init; }
    public int CookTimeMinutes { get; init; }
    public int Serving { get; init; }
    public Difficulty Difficulty { get; init; }
    public string? ImageUrl { get; init; }
    public DateTime CreatedAt { get; init; }
    public ICollection<Ingredient> Ingredients { get; init; } = [];
    public ICollection<Instruction> Instructions { get; init; } = [];
    public ICollection<MealType> MealTypes { get; init; } = [];
    public Guid UserId { get; init; }
    public User User { get; init; } = null!;
}
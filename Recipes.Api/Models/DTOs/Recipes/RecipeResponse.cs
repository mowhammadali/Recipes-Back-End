using Recipes.Api.Models.DTOs.MealTypes;
using Recipes.Api.Models.Entities;
using Recipes.Api.Models.Enums;
using Recipes.Api.Models.ValueObjects;

namespace Recipes.Api.Models.DTOs.Recipes;

public sealed record RecipeResponse
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
    public List<IngredientResponse> Ingredients { get; init; } = [];
    public List<InstructionResponse> Instructions { get; init; } = [];
    public List<MealTypeResponse> MealTypes { get; init; } = [];
    public RecipeUserResponse Author { get; init; } = null!;
}
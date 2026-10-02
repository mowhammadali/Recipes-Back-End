using Recipes.Api.Models.Enums;
using Recipes.Api.Models.ValueObjects;

namespace Recipes.Api.Models.Entities;

public sealed class Recipe
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int PrepTimeMinutes { get; set; }
    public int CookTimeMinutes { get; set; }
    public int Serving { get; set; }
    public Difficulty Difficulty { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; } = [];
    public ICollection<Instruction> Instructions { get; set; } = [];
    public ICollection<MealType> MealTypes { get; set; } = [];
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
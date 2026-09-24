using Recipes.Api.Models.Enums;

namespace Recipes.Api.Models.DTOs.Recipes;

public sealed record UpdateRecipeRequest
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int PrepTimeMinutes { get; set; }
    public int CookTimeMinutes { get; set; }
    public int Serving { get; set; }
    public Difficulty Difficulty { get; set; }
    public IFormFile? Image { get; set; }
    public List<IngredientRequest> Ingredients { get; set; } = [];
    public List<InstructionRequest> Instructions { get; set; } = [];
    public List<Guid> MealTypeIds { get; set; } = [];
};
namespace Recipes.Api.Models.DTOs.Statistics;

public sealed record RecipeCountByMealTypeResponse(Guid MealTypeId, string MealType, int RecipeCount);
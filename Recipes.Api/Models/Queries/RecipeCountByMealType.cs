namespace Recipes.Api.Models.Queries;

public sealed record RecipeCountByMealType(
    Guid MealTypeId,
    string MealType,
    int RecipeCount);
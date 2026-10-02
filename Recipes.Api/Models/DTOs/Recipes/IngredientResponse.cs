namespace Recipes.Api.Models.DTOs.Recipes;

public record IngredientResponse(
    string Name,
    decimal Quantity,
    string Unit);
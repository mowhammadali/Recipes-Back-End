namespace Recipes.Api.Models.DTOs.Recipes;

public record IngredientRequest(
    string Name,
    decimal Quantity,
    string Unit);
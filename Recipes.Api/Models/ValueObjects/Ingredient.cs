namespace Recipes.Api.Models.ValueObjects;

public sealed record Ingredient(string Name, decimal Quantity, string Unit);
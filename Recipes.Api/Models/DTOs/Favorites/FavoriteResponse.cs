using Recipes.Api.Models.DTOs.Recipes;

namespace Recipes.Api.Models.DTOs.Favorites;

public sealed record FavoriteResponse
{
    public Guid Id { get; set; }
    public RecipeResponse Recipe { get; set; } = null!;
}
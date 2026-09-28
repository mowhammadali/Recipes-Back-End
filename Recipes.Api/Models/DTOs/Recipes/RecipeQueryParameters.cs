using Recipes.Api.Models.Enums;

namespace Recipes.Api.Models.DTOs.Recipes;

public sealed record RecipeQueryParameters
{
    public int? Page { get; init; }

    public int? PageSize { get; init; }

    public string? Search { get; init; }

    public Difficulty? Difficulty { get; init; }

    public Guid? MealTypeId { get; init; }

    public string? SortBy { get; init; }

    public string? SortOrder { get; init; }
}
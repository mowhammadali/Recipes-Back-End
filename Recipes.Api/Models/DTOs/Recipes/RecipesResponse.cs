namespace Recipes.Api.Models.DTOs.Recipes;

public sealed record RecipesResponse
{
    public List<RecipeResponse> Recipes { get; set; } = [];
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
    public bool HasPreviousPage { get; init; }
    public bool HasNextPage { get; init; }
};
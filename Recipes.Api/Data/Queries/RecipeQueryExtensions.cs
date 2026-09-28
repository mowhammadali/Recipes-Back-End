using Recipes.Api.Models.DTOs.Recipes;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Queries;

public static class RecipeQueryExtensions
{
    public static IQueryable<Recipe> ApplyFiltering(this IQueryable<Recipe> query, RecipeQueryParameters parameters)
    {
        if (!string.IsNullOrWhiteSpace(parameters.Search))
        {
            query = query.Where(q => q.Name.Contains(parameters.Search) || q.Description.Contains(parameters.Search));
        }

        if (parameters.Difficulty.HasValue)
        {
            query = query.Where(q => q.Difficulty == parameters.Difficulty.Value);
        }

        if (parameters.MealTypeId.HasValue)
        {
            query = query.Where(q => q.MealTypes.Any(m => m.Id == parameters.MealTypeId.Value));
        }

        return query;
    }

    public static IQueryable<Recipe> ApplySorting(this IQueryable<Recipe> query, RecipeQueryParameters parameters)
    {
        var sortBy = parameters.SortBy?.ToLower();
        var sortOrder = parameters.SortOrder?.ToLower();

        return sortBy switch
        {
            "name" => sortOrder == "asc"
                ? query.OrderBy(q => q.Name)
                : query.OrderByDescending(q => q.Name),

            "createdat" => sortOrder == "asc"
                ? query.OrderBy(x => x.CreatedAt)
                : query.OrderByDescending(x => x.CreatedAt),

            "preptime" => sortOrder == "asc"
                ? query.OrderBy(x => x.PrepTimeMinutes)
                : query.OrderByDescending(x => x.PrepTimeMinutes),

            "cooktime" => sortOrder == "asc"
                ? query.OrderBy(x => x.CookTimeMinutes)
                : query.OrderByDescending(x => x.CookTimeMinutes),

            _ => query.OrderByDescending(q => q.CreatedAt)
        };
    }

    public static IQueryable<Recipe> ApplyPagination(
        this IQueryable<Recipe> query,
        RecipeQueryParameters parameters)
    {
        var page = parameters.Page ?? 1;
        var pageSize = parameters.PageSize ?? 10;

        return query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}
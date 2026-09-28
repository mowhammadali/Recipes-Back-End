namespace Recipes.Api.Models.DTOs.Common;

public sealed record PagedResponse<T>
{
    public List<T> Items { get; init; } = [];

    public int Page { get; init; }

    public int PageSize { get; init; }

    public int TotalItems { get; init; }

    public int TotalPages { get; init; }

    public bool HasPreviousPage { get; init; }

    public bool HasNextPage { get; init; }
}
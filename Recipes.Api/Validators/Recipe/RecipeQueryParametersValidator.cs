using FluentValidation;
using Recipes.Api.Models.DTOs.Recipes;

namespace Recipes.Api.Validators.Recipe;

public sealed class RecipeQueryParametersValidator
    : AbstractValidator<RecipeQueryParameters>
{
    private static readonly string[] AllowedSortFields =
    [
        "name",
        "createdAt",
        "prepTime",
        "cookTime"
    ];

    public RecipeQueryParametersValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .When(x => x.Page.HasValue)
            .WithMessage("Page must be greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 50)
            .When(x => x.PageSize.HasValue)
            .WithMessage("Page size must be between 1 and 50.");

        RuleFor(x => x.SortBy)
            .Must(BeValidSortField)
            .When(x => !string.IsNullOrWhiteSpace(x.SortBy))
            .WithMessage(
                "SortBy must be one of: name, createdAt, prepTime, cookTime.");

        RuleFor(x => x.SortOrder)
            .Must(x =>
                x is null ||
                x.Equals("asc", StringComparison.OrdinalIgnoreCase) ||
                x.Equals("desc", StringComparison.OrdinalIgnoreCase))
            .WithMessage("SortOrder must be either 'asc' or 'desc'.");
    }

    private static bool BeValidSortField(string? sortBy)
    {
        return AllowedSortFields.Contains(
            sortBy,
            StringComparer.OrdinalIgnoreCase);
    }
}
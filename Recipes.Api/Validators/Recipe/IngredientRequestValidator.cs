using System.Data;
using FluentValidation;
using Recipes.Api.Models.DTOs.Recipes;

namespace Recipes.Api.Validators.Recipe;

public class IngredientRequestValidator : AbstractValidator<IngredientRequest>
{
    public IngredientRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.Unit)
            .NotEmpty()
            .MaximumLength(30);
    }
}
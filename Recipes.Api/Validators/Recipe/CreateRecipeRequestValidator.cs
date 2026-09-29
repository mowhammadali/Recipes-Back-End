using FluentValidation;
using Recipes.Api.Models.DTOs.Recipes;

namespace Recipes.Api.Validators.Recipe;

public class CreateRecipeRequestValidator : AbstractValidator<CreateRecipeRequest>
{
    public CreateRecipeRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(x => x.PrepTimeMinutes)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.CookTimeMinutes)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Serving)
            .GreaterThan(0);

        RuleFor(x => x.Ingredients)
            .NotEmpty()
            .WithMessage("Recipe must contain at least one ingredient.");

        RuleForEach(x => x.Ingredients)
            .SetValidator(new IngredientRequestValidator());

        RuleFor(x => x.Instructions)
            .NotEmpty()
            .WithMessage("Recipe must contain at least one instruction.");

        RuleForEach(x => x.Instructions)
            .SetValidator(new InstructionRequestValidator());

        RuleFor(x => x.MealTypeIds)
            .NotEmpty()
            .WithMessage("Recipe must have at least one meal type.");

        RuleFor(x => x.MealTypeIds)
            .Must(x => x.Distinct().Count() == x.Count)
            .WithMessage("Meal types cannot contain duplicate IDs.");
    }
}
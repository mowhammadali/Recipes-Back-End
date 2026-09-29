using FluentValidation;
using Recipes.Api.Models.DTOs.Recipes;

namespace Recipes.Api.Validators.Recipe;

public class InstructionRequestValidator : AbstractValidator<InstructionRequest>
{
    public InstructionRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);
    }
}
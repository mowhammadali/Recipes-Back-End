using FluentValidation;
using Recipes.Api.Models.DTOs.Users;

namespace Recipes.Api.Validators.User;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(r => r.Username)
            .NotEmpty()
            .NotNull()
            .WithMessage("Username is required")
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("Email is required")
            .EmailAddress();
    }
}
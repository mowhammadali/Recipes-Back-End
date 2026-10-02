using FluentValidation;
using Recipes.Api.Models.DTOs.Common;

namespace Recipes.Api.Validators.File;

public sealed class UploadImageRequestValidator : AbstractValidator<UploadImageRequest>
{
    private static readonly string[] AllowedExtensions =
    [
        ".jpg",
        ".jpeg",
        ".png",
        ".webp"
    ];

    private static readonly string[] AllowedContentTypes =
    [
        "image/jpeg",
        "image/png",
        "image/webp"
    ];

    private const long MaxFileSize = 5 * 1024 * 1024;

    public UploadImageRequestValidator()
    {
        RuleFor(x => x.File)
            .NotNull()
            .WithMessage("Image file is required.");

        RuleFor(x => x.File)
            .Must(file => file is null || file.Length <= MaxFileSize)
            .WithMessage("Image size must not exceed 5 MB.");

        RuleFor(x => x.File)
            .Must(file =>
                file is null ||
                AllowedExtensions.Contains(
                    Path.GetExtension(file.FileName).ToLowerInvariant()))
            .WithMessage("Only JPG, JPEG, PNG, and WEBP images are allowed.");

        RuleFor(x => x.File)
            .Must(file =>
                file is null ||
                AllowedContentTypes.Contains(
                    file.ContentType.ToLowerInvariant()))
            .WithMessage("Invalid image content type.");
    }
}
using System.ComponentModel.DataAnnotations;

namespace Recipes.Api.Models.DTOs.Auth;

public sealed record RegisterRequest
{
    [Display(Name = "Username")]
    [Required(ErrorMessage = "{0} is required.")]
    public string Username { get; set; } = null!;

    [Display(Name = "Email")]
    [Required(ErrorMessage = "{0} is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password")]
    [Required(ErrorMessage = "{0} is required.")]
    [MinLength(8, ErrorMessage = "{0} must be at least {1} characters.")]
    public string Password { get; set; } = null!;
}
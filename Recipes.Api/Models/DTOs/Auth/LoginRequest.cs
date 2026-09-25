using System.ComponentModel.DataAnnotations;

namespace Recipes.Api.Models.DTOs.Auth;

public sealed record LoginRequest
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "{0} is required.")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password")]
    [Required(ErrorMessage = "{0} is required.")]
    public string Password { get; set; } = null!;
}
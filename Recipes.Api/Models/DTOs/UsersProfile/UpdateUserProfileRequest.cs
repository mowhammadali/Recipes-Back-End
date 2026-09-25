using System.ComponentModel.DataAnnotations;

namespace Recipes.Api.Models.DTOs.UsersProfile;

public sealed record UpdateUserProfileRequest
{
    [Display(Name = "First Name")]
    [StringLength(50, ErrorMessage = "{0} Cannot be longer than {1} characters.")]
    public string? FirstName { get; set; }

    [Display(Name = "Last Name")]
    [StringLength(50, ErrorMessage = "{0} Cannot be longer than {1} characters.")]
    public string? LastName { get; set; }

    [Display(Name = "Bio")]
    [StringLength(100, ErrorMessage = "{0} Cannot be longer than {1} characters.")]
    public string? Bio { get; set; }
}
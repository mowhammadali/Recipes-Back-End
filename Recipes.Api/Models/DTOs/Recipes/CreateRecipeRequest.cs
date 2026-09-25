using System.ComponentModel.DataAnnotations;
using Recipes.Api.Models.Enums;

namespace Recipes.Api.Models.DTOs.Recipes;

public sealed record CreateRecipeRequest
{
    [Display(Name = "Recipe Name")]
    [Required(ErrorMessage = "{0} is required.")]
    [StringLength(50, ErrorMessage = "{0} Cannot be longer than {1} characters.")]
    public string Name { get; set; } = null!;

    [Display(Name = "Description")]
    [Required(ErrorMessage = "{0} is required.")]
    [StringLength(250, ErrorMessage = "{0} Cannot be longer than {1} characters.")]
    public string Description { get; set; } = null!;

    [Display(Name = "Preparation Time")]
    [Range(1, 1440, ErrorMessage = "{0} must be between {1} and {2} minutes.")]
    [Required(ErrorMessage = "{0} is required.")]
    public int PrepTimeMinutes { get; set; }

    [Display(Name = "Cook Time")]
    [Range(1, 1440, ErrorMessage = "{0} must be between {1} and {2} minutes.")]
    [Required(ErrorMessage = "{0} is required.")]
    public int CookTimeMinutes { get; set; }

    [Display(Name = "Serving")]
    [Range(0, 10, ErrorMessage = "{0} must be between {1} and {2}.")]
    [Required(ErrorMessage = "{0} is required.")]
    public int Serving { get; set; }

    [Display(Name = "Difficulty")]
    [Required(ErrorMessage = "{0} is required.")]
    public Difficulty Difficulty { get; set; }

    [Display(Name = "Image URL")]
    [StringLength(400, ErrorMessage = "{0} Cannot be longer than {1} characters.")]
    public IFormFile? Image { get; set; }

    public List<IngredientRequest> Ingredients { get; set; } = [];
    public List<InstructionRequest> Instructions { get; set; } = [];
    public List<Guid> MealTypeIds { get; set; } = [];
}
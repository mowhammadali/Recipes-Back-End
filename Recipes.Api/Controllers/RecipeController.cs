using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Recipes.Api.Models.DTOs.Common;
using Recipes.Api.Models.DTOs.Recipes;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    private readonly IValidator<RecipeQueryParameters> _validator;

    public RecipeController(IRecipeService recipeService, IValidator<RecipeQueryParameters> validator)
    {
        _recipeService = recipeService;
        _validator = validator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<RecipeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PagedResponse<RecipeResponse>>> GetAllAsync(
        [FromQuery] RecipeQueryParameters queryParameters)
    {
        var validationResult = _validator.Validate(queryParameters);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var response = await _recipeService.GetAllAsync(queryParameters);

        return Ok(response);
    }
}
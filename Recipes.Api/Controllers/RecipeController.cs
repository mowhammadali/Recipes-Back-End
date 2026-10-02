using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Api.Exceptions;
using Recipes.Api.Models.DTOs.Common;
using Recipes.Api.Models.DTOs.Recipes;
using Recipes.Api.Models.DTOs.Statistics;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    private readonly IValidator<RecipeQueryParameters> _recipeQueryParametersValidator;
    private readonly IValidator<CreateRecipeRequest> _createRecipeRequestValidator;

    public RecipeController(IRecipeService recipeService,
        IValidator<RecipeQueryParameters> recipeQueryParametersvalidator,
        IValidator<CreateRecipeRequest> createRecipeRequestValidator)
    {
        _recipeService = recipeService;
        _recipeQueryParametersValidator = recipeQueryParametersvalidator;
        _createRecipeRequestValidator = createRecipeRequestValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<RecipeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PagedResponse<RecipeResponse>>> GetAllAsync(
        [FromQuery] RecipeQueryParameters queryParameters)
    {
        var validationResult = _recipeQueryParametersValidator.Validate(queryParameters);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var response = await _recipeService.GetAllAsync(queryParameters);

        return Ok(response);
    }

    [HttpGet("{id:guid}", Name = "GetRecipeById")]
    [ProducesResponseType(typeof(RecipeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RecipeResponse>> GetByIdAsync([FromRoute] Guid id)
    {
        var response = await _recipeService.GetByIdAsync(id);

        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateRecipeRequest request)
    {
        var userClaimId = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userClaimId is null || !Guid.TryParse(userClaimId.Value, out var userId))
        {
            throw new UnauthorizedException(
                "Invalid user identity.");
        }

        var validationResult = await _createRecipeRequestValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var response = await _recipeService.AddAsync(userId, request);

        return CreatedAtRoute("GetRecipeById", new { id = response.Id }, response);
    }

    [HttpDelete("{recipeId:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid recipeId)
    {
        var userClaimId = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userClaimId is null || !Guid.TryParse(userClaimId.Value, out var userId))
        {
            throw new UnauthorizedException(
                "Invalid user identity.");
        }

        await _recipeService.DeleteAsync(recipeId, userId);
        return NoContent();
    }

    [HttpGet("statistics/by-meal-type")]
    [ProducesResponseType(
        typeof(List<RecipeCountByMealTypeResponse>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RecipeCountByMealTypeResponse>>> GetStatisticsByMealTypeAsync()
    {
        var response = await _recipeService.GetRecipeCountByMealTypeAsync();

        return Ok(response);
    }
}
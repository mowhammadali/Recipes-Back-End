using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Api.Exceptions;
using Recipes.Api.Models.DTOs.Favorites;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("api/favorites")]
public class FavoriteController : ControllerBase
{
    private readonly IFavoriteService _recipeService;

    public FavoriteController(IFavoriteService recipeService)
    {
        _recipeService = recipeService;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(List<FavoriteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<FavoriteResponse>>> GetAllAsync()
    {
        var userClaimId = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userClaimId is null || !Guid.TryParse(userClaimId.Value, out var userId))
        {
            throw new UnauthorizedException(
                "Invalid user identity.");
        }

        var response = await _recipeService.GetAllAsync(userId);
        return Ok(response);
    }

    [HttpPost("{recipeId:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateFavorite([FromRoute] Guid recipeId)
    {
        var userClaimId = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userClaimId is null || !Guid.TryParse(userClaimId.Value, out var userId))
        {
            throw new UnauthorizedException(
                "Invalid user identity.");
        }

        await _recipeService.AddFavoriteAsync(userId, recipeId);

        return Created();
    }
}
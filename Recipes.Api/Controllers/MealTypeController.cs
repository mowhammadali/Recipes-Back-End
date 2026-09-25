using Microsoft.AspNetCore.Mvc;
using Recipes.Api.Models.DTOs.MealTypes;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealTypeController : ControllerBase
{
    private readonly IMealTypeService _mealTypeService;

    public MealTypeController(IMealTypeService mealTypeService)
    {
        _mealTypeService = mealTypeService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<MealTypeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var mealTypes = await _mealTypeService.GetAllAsync();

        return Ok(mealTypes);
    }
}
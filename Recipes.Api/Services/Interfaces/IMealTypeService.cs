using Recipes.Api.Models.DTOs.MealTypes;

namespace Recipes.Api.Services.Interfaces;

public interface IMealTypeService
{
    Task<List<MealTypeResponse>> GetAllAsync();
}
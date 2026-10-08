using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Repositories.Interfaces;

public interface IDapperMealTypeRepository
{
    Task<List<MealType>> GetAllAsync();
}
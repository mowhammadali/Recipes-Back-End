using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Repositories;

public interface IMealTypeRepository
{
    Task<List<MealType>> GetAllAsync();
}
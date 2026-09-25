using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Repositories.Interfaces;

public interface IMealTypeRepository
{
    Task<List<MealType>> GetAllAsync();
}
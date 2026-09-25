namespace Recipes.Api.Data.Repositories.Interfaces;

public interface IUnitOfWork
{
    IRecipeRepository Recipes { get; }
    IMealTypeRepository MealTypes { get; }

    Task<int> SaveChangesAsync();
}
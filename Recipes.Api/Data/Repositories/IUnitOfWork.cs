namespace Recipes.Api.Data.Repositories;

public interface IUnitOfWork
{
    IRecipeRepository Recipes { get; }
    IMealTypeRepository MealTypes { get; }

    Task<int> SaveChangesAsync();
}
namespace Recipes.Api.Data.Repositories.Interfaces;

public interface IUnitOfWork
{
    IRecipeRepository Recipes { get; }
    IMealTypeRepository MealTypes { get; }
    IUserRepository Users { get; }
    IFavoriteRepository Favorites { get; }

    Task<int> SaveChangesAsync();
}
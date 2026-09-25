namespace Recipes.Api.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public IRecipeRepository Recipes { get; }
    public IMealTypeRepository MealTypes { get; }

    public UnitOfWork(IRecipeRepository recipes, IMealTypeRepository mealTypes, AppDbContext dbContext)
    {
        Recipes = recipes;
        MealTypes = mealTypes;
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
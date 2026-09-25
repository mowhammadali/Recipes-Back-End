using Recipes.Api.Data.Repositories.Interfaces;

namespace Recipes.Api.Data.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public IRecipeRepository Recipes { get; }
    public IMealTypeRepository MealTypes { get; }
    public IUserRepository Users { get; }

    public UnitOfWork(IRecipeRepository recipes, IMealTypeRepository mealTypes, AppDbContext dbContext,
        IUserRepository users)
    {
        Recipes = recipes;
        MealTypes = mealTypes;
        _dbContext = dbContext;
        Users = users;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
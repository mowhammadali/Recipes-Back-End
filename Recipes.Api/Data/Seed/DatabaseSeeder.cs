using Microsoft.EntityFrameworkCore;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext dbContext, ILogger logger)
    {
        if (!await dbContext.Database.CanConnectAsync())
        {
            logger.LogWarning(
                "Database is unavailable. Skipping database seeding.");
            return;
        }

        if (await dbContext.MealTypes.AnyAsync())
        {
            logger.LogInformation(
                "MealTypes table already contains data. Skipping database seeding.");

            return;
        }

        var mealTypes = new[]
        {
            new MealType
            {
                Id = Guid.NewGuid(),
                Name = "Breakfast"
            },
            new MealType
            {
                Id = Guid.NewGuid(),
                Name = "Lunch"
            },
            new MealType
            {
                Id = Guid.NewGuid(),
                Name = "Dinner"
            },
            new MealType
            {
                Id = Guid.NewGuid(),
                Name = "Dessert"
            },
            new MealType
            {
                Id = Guid.NewGuid(),
                Name = "Snack"
            },
            new MealType
            {
                Id = Guid.NewGuid(),
                Name = "Appetizer"
            },
            new MealType
            {
                Id = Guid.NewGuid(),
                Name = "Beverage"
            }
        };

        await dbContext.MealTypes.AddRangeAsync(mealTypes);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "MealTypes seeded successfully.");
    }
}
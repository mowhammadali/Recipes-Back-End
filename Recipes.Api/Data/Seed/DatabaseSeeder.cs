using Microsoft.EntityFrameworkCore;
using Recipes.Api.Models.Entities;
using Recipes.Api.Models.Enums;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Data.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(
        AppDbContext dbContext,
        ILogger logger,
        IPasswordHasher passwordHasher)
    {
        try
        {
            if (!await dbContext.Database.CanConnectAsync())
            {
                logger.LogWarning(
                    "Database is unavailable. Skipping database seeding.");

                return;
            }

            await SeedMealTypesAsync(dbContext, logger);

            await SeedAdminUserAsync(
                dbContext,
                logger,
                passwordHasher);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "An error occurred while seeding the database.");
        }
    }

    public static async Task SeedMealTypesAsync(AppDbContext dbContext, ILogger logger)
    {
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

    public static async Task SeedAdminUserAsync(AppDbContext dbContext, ILogger logger, IPasswordHasher passwordHasher)
    {
        var adminExists = await dbContext.Users.AnyAsync(x => x.Role == UserRole.Admin);

        if (adminExists)
        {
            logger.LogInformation(
                "Admin user already exists. " +
                "Skipping admin seeding.");

            return;
        }

        const string adminPassword = "Admin123";

        var admin = new User
        {
            Id = Guid.NewGuid(),
            Username = "admin",
            Email = "admin@gmail.com",
            PasswordHash = passwordHasher.Hash(adminPassword),
            Role = UserRole.Admin,
            CreatedAt = DateTime.UtcNow
        };

        await dbContext.Users.AddAsync(admin);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Admin user seeded successfully. Username: {Username}",
            admin.Username);
    }
}
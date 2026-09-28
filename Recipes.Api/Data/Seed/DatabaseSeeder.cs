using Microsoft.EntityFrameworkCore;
using Recipes.Api.Models.Entities;
using Recipes.Api.Models.Enums;
using Recipes.Api.Models.ValueObjects;
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

            await SeedRecipesAsync(dbContext, logger);
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
            CreatedAt = DateTime.UtcNow,
            UserProfile = new UserProfile()
            {
                Id = Guid.NewGuid()
            },
        };

        await dbContext.Users.AddAsync(admin);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Admin user seeded successfully. Username: {Username}",
            admin.Username);
    }

    public static async Task SeedRecipesAsync(
        AppDbContext dbContext,
        ILogger logger)
    {
        if (await dbContext.Recipes.AnyAsync())
        {
            logger.LogInformation(
                "Recipes table already contains data. Skipping recipe seeding.");

            return;
        }

        var admin = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Role == UserRole.Admin);

        if (admin is null)
        {
            logger.LogWarning(
                "Admin user was not found. Skipping recipe seeding.");

            return;
        }

        var mealTypes = await dbContext.MealTypes
            .Where(x =>
                x.Name == "Breakfast" ||
                x.Name == "Lunch" ||
                x.Name == "Dinner" ||
                x.Name == "Dessert")
            .ToListAsync();

        if (mealTypes.Count == 0)
        {
            logger.LogWarning(
                "Required meal types were not found. Skipping recipe seeding.");

            return;
        }

        var breakfast = mealTypes.FirstOrDefault(x => x.Name == "Breakfast");
        var lunch = mealTypes.FirstOrDefault(x => x.Name == "Lunch");
        var dinner = mealTypes.FirstOrDefault(x => x.Name == "Dinner");
        var dessert = mealTypes.FirstOrDefault(x => x.Name == "Dessert");

        if (breakfast is null ||
            lunch is null ||
            dinner is null ||
            dessert is null)
        {
            logger.LogWarning(
                "One or more required meal types are missing. " +
                "Skipping recipe seeding.");

            return;
        }

        var recipes = new[]
        {
            new Recipe
            {
                Id = Guid.NewGuid(),
                Name = "Chicken Pasta",
                Description =
                    "Creamy chicken pasta with garlic and parmesan cheese.",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 25,
                Serving = 4,
                Difficulty = Difficulty.Easy,
                ImageUrl = null,
                CreatedAt = DateTime.UtcNow,
                UserId = admin.Id,

                Ingredients =
                [
                    new Ingredient("Chicken breast", 500, "g"),
                    new Ingredient("Pasta", 300, "g"),
                    new Ingredient("Heavy cream", 200, "ml"),
                    new Ingredient("Parmesan cheese", 80, "g"),
                    new Ingredient("Garlic", 3, "cloves"),
                    new Ingredient("Olive oil", 2, "tbsp"),
                    new Ingredient("Salt", 1, "tsp"),
                    new Ingredient("Black pepper", 0.5m, "tsp")
                ],

                Instructions =
                [
                    new Instruction(
                        1,
                        "Cook the pasta according to the package instructions."),

                    new Instruction(
                        2,
                        "Cut the chicken breast into small pieces and season with salt and pepper."),

                    new Instruction(
                        3,
                        "Heat olive oil in a large pan and cook the chicken until golden."),

                    new Instruction(
                        4,
                        "Add minced garlic and cook for one minute."),

                    new Instruction(
                        5,
                        "Add heavy cream and parmesan cheese, then stir until the sauce thickens."),

                    new Instruction(
                        6,
                        "Add the cooked pasta and mix well."),

                    new Instruction(
                        7,
                        "Serve immediately with extra parmesan cheese.")
                ],

                MealTypes =
                [
                    lunch,
                    dinner
                ]
            },

            new Recipe
            {
                Id = Guid.NewGuid(),
                Name = "Avocado Toast",
                Description =
                    "Simple and healthy avocado toast with eggs and fresh vegetables.",
                PrepTimeMinutes = 10,
                CookTimeMinutes = 5,
                Serving = 2,
                Difficulty = Difficulty.Easy,
                ImageUrl = null,
                CreatedAt = DateTime.UtcNow,
                UserId = admin.Id,

                Ingredients =
                [
                    new Ingredient("Whole grain bread", 2, "slices"),
                    new Ingredient("Avocado", 1, "piece"),
                    new Ingredient("Eggs", 2, "pieces"),
                    new Ingredient("Cherry tomatoes", 100, "g"),
                    new Ingredient("Lemon juice", 1, "tbsp"),
                    new Ingredient("Salt", 0.5m, "tsp"),
                    new Ingredient("Black pepper", 0.25m, "tsp")
                ],

                Instructions =
                [
                    new Instruction(
                        1,
                        "Toast the bread slices until golden and crispy."),

                    new Instruction(
                        2,
                        "Mash the avocado with lemon juice, salt, and black pepper."),

                    new Instruction(
                        3,
                        "Cook the eggs according to your preference."),

                    new Instruction(
                        4,
                        "Spread the mashed avocado over the toasted bread."),

                    new Instruction(
                        5,
                        "Top with the eggs and sliced cherry tomatoes."),

                    new Instruction(
                        6,
                        "Season with additional black pepper and serve.")
                ],

                MealTypes =
                [
                    breakfast
                ]
            },

            new Recipe
            {
                Id = Guid.NewGuid(),
                Name = "Chocolate Brownies",
                Description =
                    "Rich and fudgy chocolate brownies with a soft center.",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 30,
                Serving = 12,
                Difficulty = Difficulty.Medium,
                ImageUrl = null,
                CreatedAt = DateTime.UtcNow,
                UserId = admin.Id,

                Ingredients =
                [
                    new Ingredient("Dark chocolate", 200, "g"),
                    new Ingredient("Butter", 150, "g"),
                    new Ingredient("Sugar", 180, "g"),
                    new Ingredient("Eggs", 3, "pieces"),
                    new Ingredient("All-purpose flour", 100, "g"),
                    new Ingredient("Cocoa powder", 30, "g"),
                    new Ingredient("Vanilla extract", 1, "tsp"),
                    new Ingredient("Salt", 0.25m, "tsp")
                ],

                Instructions =
                [
                    new Instruction(
                        1,
                        "Preheat the oven to 180°C."),

                    new Instruction(
                        2,
                        "Melt the dark chocolate and butter together over low heat."),

                    new Instruction(
                        3,
                        "Whisk the eggs, sugar, and vanilla extract until well combined."),

                    new Instruction(
                        4,
                        "Pour the melted chocolate mixture into the egg mixture and stir."),

                    new Instruction(
                        5,
                        "Add the flour, cocoa powder, and salt, then mix until just combined."),

                    new Instruction(
                        6,
                        "Pour the batter into a lined baking pan."),

                    new Instruction(
                        7,
                        "Bake for about 30 minutes until the edges are set but the center remains slightly soft."),

                    new Instruction(
                        8,
                        "Allow the brownies to cool before cutting into pieces.")
                ],

                MealTypes =
                [
                    dessert
                ]
            }
        };

        await dbContext.Recipes.AddRangeAsync(recipes);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Recipes seeded successfully. Count: {Count}",
            recipes.Length);
    }
}
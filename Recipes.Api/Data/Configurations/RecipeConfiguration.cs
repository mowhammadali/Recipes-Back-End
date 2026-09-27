using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Configurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.ToTable("Recipes");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name).HasMaxLength(50).IsRequired();

        builder.Property(r => r.Description).HasMaxLength(250).IsRequired();

        builder.Property(r => r.CookTimeMinutes).IsRequired();

        builder.Property(r => r.PrepTimeMinutes).IsRequired();

        builder.Property(r => r.Serving).IsRequired();

        builder.Property(r => r.Difficulty)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(r => r.ImageUrl).HasMaxLength(400);

        builder.Property(r => r.CreatedAt).IsRequired();

        builder.OwnsMany(r => r.Ingredients, ingredient =>
        {
            ingredient.ToTable("RecipeIngredients");

            ingredient.Property(i => i.Name).IsRequired();
            ingredient.Property(i => i.Quantity).IsRequired();
            ingredient.Property(i => i.Unit).IsRequired();
        });

        // builder.Property(r => r.Instructions).IsRequired();
        builder.OwnsMany(r => r.Instructions, instruction =>
        {
            instruction.ToTable("RecipeInstructions");

            instruction.Property(x => x.Description)
                .IsRequired();
        });

        builder.HasOne(r => r.User)
            .WithMany(x => x.Recipes)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
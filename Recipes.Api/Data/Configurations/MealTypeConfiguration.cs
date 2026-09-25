using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Configurations;

public class MealTypeConfiguration : IEntityTypeConfiguration<MealType>
{
    public void Configure(EntityTypeBuilder<MealType> builder)
    {
        builder.ToTable("MealTypes");
        builder.HasKey(x => x.Id);

        builder.Property(m => m.Name).IsRequired().HasMaxLength(50);

        builder.HasMany(m => m.Recipes)
            .WithMany(r => r.MealTypes)
            .UsingEntity("RecipeMealType");
    }
}
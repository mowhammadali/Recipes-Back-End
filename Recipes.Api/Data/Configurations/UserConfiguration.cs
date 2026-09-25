using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Api.Models.Entities;

namespace Recipes.Api.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);

        builder.Property(u => u.Username).HasMaxLength(60).IsRequired();

        builder.Property(u => u.Email).HasMaxLength(80).IsRequired();

        builder.Property(u => u.PasswordHash).HasMaxLength(500).IsRequired();

        builder.Property(u => u.CreatedAt).IsRequired();

        builder.HasIndex(u => u.Username).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasOne(u => u.UserProfile)
            .WithOne(u => u.User)
            .HasForeignKey<UserProfile>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
using AmourLink.Infrastructure.Extensions;
using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.Data.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.Recommendation.Data.Context.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PRIMARY");

        builder.ToTable("user");
        
        builder.HasIndex(e => e.Email, "email_UNIQUE").IsUnique();

        builder.Property(e => e.Id)
            .HasColumnName("user_id");
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime(6)");
        builder.Property(e => e.Email)
            .HasMaxLength(50);
        builder.Property(e => e.Password)
            .HasColumnName("password")
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(e => e.AccountType)
            .HasConversion(
                v => v.ToString().ToUpperInvariant(),
                v => (AccountType)Enum.Parse(typeof(AccountType), v.ToPascalCase()));
    }
}
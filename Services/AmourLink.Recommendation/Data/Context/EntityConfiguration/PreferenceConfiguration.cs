using AmourLink.Infrastructure.Extensions;
using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.Data.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.Recommendation.Data.Context.EntityConfiguration;

public class PreferenceConfiguration : IEntityTypeConfiguration<Preference>
{
    public void Configure(EntityTypeBuilder<Preference> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("preference");

        builder.Property(e => e.Id)
            .HasColumnName("preference_id");
        builder.Property(e => e.Gender)
            .HasMaxLength(45);
        builder.Property(e => e.Gender)
            .HasConversion(
                v => v.ToString().ToUpperInvariant(),
                v => (GenderPreference)Enum.Parse(typeof(GenderPreference), v.ToPascalCase()));

        builder.HasOne(p => p.User)
            .WithOne(u => u.UserPreference)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
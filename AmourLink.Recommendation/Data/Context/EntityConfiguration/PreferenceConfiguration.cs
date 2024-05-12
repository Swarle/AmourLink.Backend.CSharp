using AmourLink.Recommendation.Data.Entities;
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
            .HasColumnName("preference_id")
            .HasColumnType("binary(16)");
        builder.Property(e => e.Gender)
            .HasMaxLength(45);
        builder.Property(e => e.DistanceRange)
            .HasPrecision(4,2);
        
    }
}
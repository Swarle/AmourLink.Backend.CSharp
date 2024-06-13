using AmourLink.Matching.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.Matching.Data.Context.EntityConfigurations;

public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PRIMARY");

        builder.Property(e => e.Id)
            .HasColumnName("match_id");

        builder.Property(e => e.CreatedAt)
            .HasColumnType("timestamp");
    }
}
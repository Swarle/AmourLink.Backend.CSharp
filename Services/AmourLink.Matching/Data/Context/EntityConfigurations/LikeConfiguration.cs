using AmourLink.Matching.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.Matching.Data.Context.EntityConfigurations;

public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PRIMARY");

        builder.Property(e => e.Id)
            .HasColumnName("like_id");
        
        builder.Property(e => e.CreatedAt)
            .HasColumnType("timestamp");

        builder.Property(e => e.LikeType)
            .HasConversion(
                v => v.ToString(),
                v => (LikeType)Enum.Parse(typeof(LikeType), v));
    }
}
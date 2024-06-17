using AmourLink.Swipe.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.Swipe.Data.Context.EntityConfigurations;

public class SwipeConfiguration : IEntityTypeConfiguration<Entities.SwipeEntity>
{
    public void Configure(EntityTypeBuilder<Entities.SwipeEntity> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PRIMARY");

        builder.ToTable("swipe");

        builder.Property(e => e.Id)
            .HasColumnName("swipe_id");
        
        builder.Property(e => e.CreatedAt)
            .HasColumnType("timestamp");

        builder.Property(e => e.SwipeType)
            .HasConversion(
                v => v.ToString(),
                v => (SwipeType)Enum.Parse(typeof(SwipeType), v));
    }
}
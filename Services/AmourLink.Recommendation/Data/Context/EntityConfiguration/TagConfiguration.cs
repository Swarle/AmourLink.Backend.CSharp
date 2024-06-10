using AmourLink.Recommendation.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.Recommendation.Data.Context.EntityConfiguration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PRIMARY");

        builder.ToTable("tag");
        
        builder.Property(e => e.Id)
            .HasColumnName("tag_id");
        
        builder.HasMany(d => d.UserDetails)
            .WithMany(p => p.Tags)
            .UsingEntity("user_details_tag",
                l => l.HasOne(typeof(UserDetails))
                    .WithMany()
                    .HasForeignKey("user_id"),
                r => r.HasOne(typeof(Tag))
                    .WithMany()
                    .HasForeignKey("tag_id"));
    }
}
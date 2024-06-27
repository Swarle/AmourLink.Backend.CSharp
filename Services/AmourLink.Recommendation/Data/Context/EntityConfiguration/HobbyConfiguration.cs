using AmourLink.Recommendation.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.Recommendation.Data.Context.EntityConfiguration;

public class HobbyConfiguration : IEntityTypeConfiguration<Hobby>
{
    public void Configure(EntityTypeBuilder<Hobby> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PRIMARY");
        
        builder.ToTable("hobby");
        
        builder.Property(e => e.Id)
            .HasColumnName("hobby_id");
        builder.Property(e => e.HobbyName)
            .HasMaxLength(45);

        builder.HasMany(d => d.UserDetails)
            .WithMany(p => p.Hobbies)
            .UsingEntity("user_details_hobby",
                l => l.HasOne(typeof(UserDetails))
                    .WithMany()
                    .HasForeignKey("user_id"),
                r => r.HasOne(typeof(Hobby))
                    .WithMany()
                    .HasForeignKey("hobby_id"));
    }
}
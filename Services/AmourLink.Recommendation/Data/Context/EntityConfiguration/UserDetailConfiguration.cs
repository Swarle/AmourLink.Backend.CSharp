using AmourLink.Infrastructure.Extensions;
using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.Data.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.Recommendation.Data.Context.EntityConfiguration;

public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetails>
{
    public void Configure(EntityTypeBuilder<UserDetails> builder)
    {
        builder.HasKey(d => d.Id).HasName("PRIMARY");
        
        builder.ToTable("user_details");
        
        builder.Property(e => e.Id)
            .HasColumnName("user_id");
        builder.Property(e => e.FirstName)
            .HasColumnName("firstname");
        builder.Property(e => e.LastName)
            .HasColumnName("lastname");
        builder.Property(e => e.LastName)
            .HasMaxLength(45);
        builder.Property(e => e.Nationality)
            .HasMaxLength(45);
        builder.Property(e => e.Occupation)
            .HasMaxLength(45);
        builder.Property(e => e.Gender)
            .HasConversion(
                v => v.ToString().ToUpperInvariant(),
                v => (Gender)Enum.Parse(typeof(Gender), v.ToPascalCase()));

        builder.HasOne(d => d.Music)
            .WithMany(p => p.UserDetails)
            .HasForeignKey(d => d.MusicId);

        builder.HasOne(d => d.User)
            .WithOne(p => p.UserDetails)
            .HasForeignKey<UserDetails>(d => d.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
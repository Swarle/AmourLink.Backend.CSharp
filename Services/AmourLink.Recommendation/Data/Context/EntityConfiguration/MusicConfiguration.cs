using AmourLink.Recommendation.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.Recommendation.Data.Context.EntityConfiguration;

public class MusicConfiguration : IEntityTypeConfiguration<Music>
{
    public void Configure(EntityTypeBuilder<Music> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        
        builder.ToTable("music");
        
        builder.Property(e => e.Id)
            .HasColumnName("music_id");
        builder.Property(e => e.ArtistName)
            .HasMaxLength(200);
        builder.Property(e => e.SpotifyId)
            .HasMaxLength(22);
        builder.Property(e => e.Title)
            .HasMaxLength(200);
    }
}
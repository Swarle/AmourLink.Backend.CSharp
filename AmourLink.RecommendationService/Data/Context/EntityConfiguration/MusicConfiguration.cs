using AmourLink.RecommendationService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmourLink.RecommendationService.Data.Context.EntityConfiguration;

public class MusicConfiguration : IEntityTypeConfiguration<Music>
{
    public void Configure(EntityTypeBuilder<Music> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        
        builder.ToTable("music");
        
        builder.Property(e => e.Id)
            .HasColumnName("music_id")
            .HasColumnType("binary(16)");;
        builder.Property(e => e.ArtistName)
            .HasMaxLength(200);
        builder.Property(e => e.SpotifyId)
            .HasMaxLength(22);
        builder.Property(e => e.Title)
            .HasMaxLength(200);
    }
}
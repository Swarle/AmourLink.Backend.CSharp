using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.RecommendationService.Data.Entities;

[Table("music")]
[Index("SpotifyId", Name = "music_name_UNIQUE", IsUnique = true)]
public partial class Music
{
    [Column("music_id", TypeName = "binary(16)")]
    public Guid Id { get; set; }

    [Column("spotify_id")]
    [StringLength(22)]
    public required string SpotifyId { get; set; }

    [Column("title")]
    [StringLength(200)]
    public required string Title { get; set; }

    [Column("artist_name")]
    [StringLength(200)]
    public required string ArtistName { get; set; }
    
    public ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
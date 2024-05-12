using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.RecommendationService.Data.Entities;

public partial class Music
{
    public Guid Id { get; set; }
    public required string SpotifyId { get; set; }
    public required string Title { get; set; }
    public required string ArtistName { get; set; }
    
    public ICollection<UserDetails> UserDetails { get; set; } = [];
}
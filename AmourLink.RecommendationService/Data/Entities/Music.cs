using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmourLink.RecommendationService.Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.RecommendationService.Data.Entities;

public class Music : Entity
{
    public required string SpotifyId { get; set; }
    public required string Title { get; set; }
    public required string ArtistName { get; set; }
    
    public ICollection<UserDetails> UserDetails { get; set; } = [];
}
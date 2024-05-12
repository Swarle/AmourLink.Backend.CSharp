using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmourLink.RecommendationService.Data.Abstract;

namespace AmourLink.RecommendationService.Data.Entities;

public class Picture : Entity
{
    public required string PictureUrl { get; set; }
    public required Guid UserDetailsId { get; set; }
    public DateTime AddedTime { get; set; }
    
    public UserDetails UserDetails { get; set; } = null!;
}
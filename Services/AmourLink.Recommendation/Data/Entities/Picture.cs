using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmourLink.Infrastructure.Data.Abstract;

namespace AmourLink.Recommendation.Data.Entities;

public class Picture : Entity
{
    public required string PictureUrl { get; set; }
    public required Guid UserDetailsId { get; set; }
    public required int Position { get; set; }
    public DateTime TimeAdded { get; set; }
    
    public UserDetails UserDetails { get; set; } = null!;
}
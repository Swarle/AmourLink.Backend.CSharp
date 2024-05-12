using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmourLink.RecommendationService.Data.Abstract;

namespace AmourLink.RecommendationService.Data.Entities;

public class Hobbie : Entity
{
    public Guid UserDetailsId { get; set; }
    public required string HobbieName { get; set; }
    
    public required UserDetails UserDetails { get; set; }
}
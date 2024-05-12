using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmourLink.RecommendationService.Data.Entities;

public class Hobbie
{
    public Guid Id { get; set; }
    public Guid UserDetailsId { get; set; }
    public required string HobbieName { get; set; }
    
    public required UserDetails UserDetails { get; set; }
}
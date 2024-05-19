using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmourLink.Recommendation.Data.Abstract;

namespace AmourLink.Recommendation.Data.Entities;

public class Hobbie : Entity
{
    public Guid UserDetailsId { get; set; }
    public required string HobbieName { get; set; }
    
    public required UserDetails UserDetails { get; set; }
}
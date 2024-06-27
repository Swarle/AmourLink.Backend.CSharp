using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmourLink.Infrastructure.Data.Abstract;

namespace AmourLink.Recommendation.Data.Entities;

public class Hobby : Entity
{
    public required string HobbyName { get; set; }
    
    public required List<UserDetails> UserDetails { get; set; }
}
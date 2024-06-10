using System.Collections;
using AmourLink.Infrastructure.Data.Abstract;

namespace AmourLink.Recommendation.Data.Entities;

public class Info : Entity
{
    public required string Title { get; set; }
    
    public ICollection<InfoAnswer> Answers { get; set; } = [];
    public ICollection<InfoUserDetails> InfoUserDetails { get; set; } = [];
}
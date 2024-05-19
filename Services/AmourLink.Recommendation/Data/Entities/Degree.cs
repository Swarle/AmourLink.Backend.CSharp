using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmourLink.Infrastructure.Data.Abstract;

namespace AmourLink.Recommendation.Data.Entities;

public class Degree : Entity
{
    public required string SchoolName { get; set; }
    public required string DegreeType { get; set; }
    public DateTime StartYear { get; set; }
    
    public List<UserDetails> UserDetails = [];

}
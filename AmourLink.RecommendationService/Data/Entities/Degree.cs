using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmourLink.RecommendationService.Data.Abstract;

namespace AmourLink.RecommendationService.Data.Entities;

public class Degree : Entity
{
    public required string SchoolName { get; set; }
    public required string DegreeType { get; set; }
    public DateTime StartYear { get; set; }
    
    public List<UserDetails> UserDetails = [];

}
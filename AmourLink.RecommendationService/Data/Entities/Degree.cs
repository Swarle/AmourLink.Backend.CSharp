using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmourLink.RecommendationService.Data.Entities;

public class Degree
{
    public Guid Id { get; set; }
    public required string SchoolName { get; set; }
    public required string DegreeType { get; set; }
    public DateTime StartYear { get; set; }
    
    public List<UserDetails> UserDetails = [];

}
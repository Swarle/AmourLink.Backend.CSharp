using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmourLink.Infrastructure.Data.Abstract;

namespace AmourLink.Recommendation.Data.Entities;

public class Degree : Entity
{
    public required string DegreeType { get; set; }
    public required string SchoolName { get; set; }
    public required string DegreeName { get; set; }
    
    public required UserDetails UserDetails { get; set; }

}
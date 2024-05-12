using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmourLink.RecommendationService.Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.RecommendationService.Data.Entities;

public class User : Entity
{
    public required string Email { get; set; }
    public DateTime CreatedTime { get; set; }
    public required int Rating { get; set; }
    public UserDetails? UserDetails { get; set; }
}
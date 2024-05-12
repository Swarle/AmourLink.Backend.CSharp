using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.RecommendationService.Data.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public DateTime CreatedTime { get; set; }
    public required int Rating { get; set; }
    public UserDetails? UserDetails { get; set; }
}
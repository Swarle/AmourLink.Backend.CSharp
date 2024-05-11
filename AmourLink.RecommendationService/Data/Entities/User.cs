using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.RecommendationService.Data.Entities;

[Table("user")]
[Index("Email", Name = "email_UNIQUE", IsUnique = true)]
public partial class User
{
    [Column("user_id", TypeName = "binary(16)")]
    public Guid Id { get; set; }

    [Column("email")]
    [StringLength(45)]
    public required string Email { get; set; }

    [Column("password_hash")]
    [StringLength(255)]
    public required string PasswordHash { get; set; }

    [Column("created_time", TypeName = "timestamp")]
    public DateTime CreatedTime { get; set; }
    
    public required UserDetail UserDetails { get; set; }
}
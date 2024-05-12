using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmourLink.RecommendationService.Data.Entities;

public class Language
{
    public Guid Id { get; set; }
    public required string LanguageName { get; set; }
    
    public ICollection<UserDetails> UserDetailsUserDetails { get; set; } = [];
}
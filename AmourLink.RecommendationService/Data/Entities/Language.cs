using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmourLink.RecommendationService.Data.Abstract;

namespace AmourLink.RecommendationService.Data.Entities;

public class Language : Entity
{
    public required string LanguageName { get; set; }
    
    public ICollection<UserDetails> UserDetailsUserDetails { get; set; } = [];
}
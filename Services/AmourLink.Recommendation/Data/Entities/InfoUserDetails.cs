namespace AmourLink.Recommendation.Data.Entities;

public class InfoUserDetails
{
    public Guid InfoId { get; set; }
    public Guid UserId { get; set; }
    public Guid AnswerId { get; set; }
    
    public required Info Info { get; set; }
    public required UserDetails UserDetails { get; set; }
    public required InfoAnswer InfoAnswer { get; set; }
}
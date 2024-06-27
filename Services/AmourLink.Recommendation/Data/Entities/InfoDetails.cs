namespace AmourLink.Recommendation.Data.Entities;

public class InfoDetails
{
    public Guid InfoId { get; set; }
    public Guid UserId { get; set; }
    public Guid AnswerId { get; set; }

    public Info Info { get; set; } = null!;
    public UserDetails UserDetails { get; set; } = null!;
    public InfoAnswer InfoAnswer { get; set; } = null!;
}
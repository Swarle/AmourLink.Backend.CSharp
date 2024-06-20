namespace AmourLink.Matching.Services.Interfaces;

public interface IMatchService
{
    public Task<ICollection<Guid>> GetMatchedUsersId(Guid userId, CancellationToken cancellationToken = default);
}
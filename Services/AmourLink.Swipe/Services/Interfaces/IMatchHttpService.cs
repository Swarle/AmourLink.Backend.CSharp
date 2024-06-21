namespace AmourLink.Swipe.Services.Interfaces;

public interface IMatchHttpService
{
    public Task<ICollection<Guid>> GetMatchedUsersId(Guid userId, CancellationToken cancellationToken = default);
}
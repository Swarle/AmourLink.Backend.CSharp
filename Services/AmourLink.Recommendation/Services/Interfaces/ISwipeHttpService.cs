using AmourLink.Recommendation.DTO;

namespace AmourLink.Recommendation.Services.Interfaces;

public interface ISwipeHttpService
{
    public Task<InteractionDto> GetInteractionsAsync(Guid userId, CancellationToken cancellationToken = default);
}
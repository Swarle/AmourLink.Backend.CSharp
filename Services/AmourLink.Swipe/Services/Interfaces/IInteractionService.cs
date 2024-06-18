using AmourLink.Swipe.DTO;

namespace AmourLink.Swipe.Services.Interfaces;

public interface IInteractionService
{
    public Task<InteractionDto> GetInteractedUsersIdAsync(Guid userId,CancellationToken cancellationToken = default);
}
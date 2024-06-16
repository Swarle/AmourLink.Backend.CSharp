using AmourLink.Swipe.Data.Entities;

namespace AmourLink.Swipe.Helpers;

public static class InteractionFactory
{
    public static Interaction CreateInteraction(Guid firstId, Guid secondId) =>
        new Interaction
        {
            FirstUserId = firstId,
            SecondUserId = secondId
        };
}
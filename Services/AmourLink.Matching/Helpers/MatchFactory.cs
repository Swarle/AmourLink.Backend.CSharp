using AmourLink.Matching.Data.Entities;

namespace AmourLink.Matching.Helpers;

public static class MatchFactory
{
    public static Match CreateMatch(Guid firstId, Guid secondId) =>
        new Match
        {
            FirstUserId = firstId,
            SecondUserId = secondId
        };
}
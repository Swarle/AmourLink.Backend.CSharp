using AmourLink.Infrastructure.Specification;
using AmourLink.Matching.Data.Entities;

namespace AmourLink.Matching.Specifications;

public sealed class MatchByUsersIdSpecification(Guid firstId, Guid secondId)
    : BaseSpecification<Match>(m =>
        (m.FirstUserId == firstId && m.SecondUserId == secondId) ||
        (m.SecondUserId == firstId && m.FirstUserId == secondId));

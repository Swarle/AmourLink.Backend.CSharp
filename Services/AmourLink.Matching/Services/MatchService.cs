using System.Net;
using AmourLink.Infrastructure.Extensions;
using AmourLink.Infrastructure.Repository;
using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.Matching.Data.Entities;
using AmourLink.Matching.Services.Interfaces;
using AmourLink.Matching.Specifications;

namespace AmourLink.Matching.Services;

public class MatchService : IMatchService
{
    private readonly IRepository<Match> _matchRepository;

    public MatchService(IRepository<Match> matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<ICollection<Guid>> GetMatchedUsersId(Guid userId, CancellationToken cancellationToken = default)
    {
        var matchSpecification = new MatchByUsersIdSpecification(userId);

        var matches = await _matchRepository.GetAllAsync(matchSpecification, cancellationToken);

        var matchedUserId = matches
            .SelectMany(m => new[] { m.FirstUserId, m.SecondUserId })
            .Where(id => id != userId)
            .Distinct()
            .ToList();

        return matchedUserId;
    }
}
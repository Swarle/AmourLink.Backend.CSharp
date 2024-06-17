using AmourLink.Infrastructure.Repository;
using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.InternalCommunication.Kafka.Messages;
using AmourLink.Matching.Data.Entities;
using AmourLink.Matching.Helpers;
using AmourLink.Matching.Specifications;

namespace AmourLink.Matching.KafkaHandlers;

public class SwipeMessageHandler : IMessageHandler<SwipeKafkaMessage>
{
    private readonly IRepository<Match> _matchRepository;
    public SwipeMessageHandler(IRepository<Match> matchRepository)
    {
        _matchRepository = matchRepository;
    }
    public async Task HandleAsync(SwipeKafkaMessage obj, CancellationToken cancellationToken = default)
    {
        var matchSpecification = new MatchByUsersIdSpecification(obj.FirstUserId, obj.SecondUserId);

        var isMatchAlreadyExist = await _matchRepository.AnyAsync(matchSpecification, cancellationToken);

        if (isMatchAlreadyExist)
            throw new Exception("Can`t create match because it already exists"); //TODO: Make exception type for exceptions in KafkaHandler
        
        var match = MatchFactory.CreateMatch(obj.FirstUserId, obj.SecondUserId);

        await _matchRepository.CreateAsync(match);
        await _matchRepository.SaveChangesAsync();
    }
}
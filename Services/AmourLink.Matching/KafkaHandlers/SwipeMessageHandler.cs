using AmourLink.Infrastructure.Repository;
using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.InternalCommunication.Kafka.Messages;
using AmourLink.Matching.Data.Entities;
using AmourLink.Matching.Helpers;

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
        var match = MatchFactory.CreateMatch(obj.FirstUserId, obj.SecondUserId);

        await _matchRepository.CreateAsync(match);
        await _matchRepository.SaveChangesAsync();
    }


}
using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.Swipe.KafkaMessages;
using AmourLink.Swipe.Services.Interfaces;
using AutoMapper;
using Confluent.Kafka;

namespace AmourLink.Swipe.Services;

public class SwipeService : ISwipeService
{
    private readonly IMapper _mapper;
    private readonly IKafkaProducer<Ignore, string, SwipeKafkaMessage> _producer;

    public SwipeService(IMapper mapper, IKafkaProducer<Ignore, string, SwipeKafkaMessage> producer)
    {
        _mapper = mapper;
        _producer = producer;
    }
    
    public async Task LikeAsync()
    {
        throw new NotImplementedException();
    }
}
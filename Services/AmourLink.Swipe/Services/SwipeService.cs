using AmourLink.InternalCommunication.Kafka;
using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.InternalCommunication.Kafka.Messages;
using AmourLink.Swipe.DTO;
using AmourLink.Swipe.Services.Interfaces;
using Confluent.Kafka;

namespace AmourLink.Swipe.Services;

public class SwipeService : ISwipeService
{
    private readonly IKafkaProducer<Null, string, SwipeKafkaMessage> _producer;

    public SwipeService(IKafkaProducer<Null, string, SwipeKafkaMessage> producer)
    {
        _producer = producer;
    }
    
    public async Task LikeAsync(SwipeDto swipeDto)
    {
        var kafkaMessage = new SwipeKafkaMessage(swipeDto.SenderId, swipeDto.ReceiverId);

        await _producer.ProduceInternalAsync(TopicNames.SwipeEvents, kafkaMessage);
    }

    public async Task DislikeAsync(SwipeDto swipeDto)
    {
        var kafkaMessage = new SwipeKafkaMessage(swipeDto.SenderId, swipeDto.ReceiverId);

        await _producer.ProduceInternalAsync(TopicNames.SwipeEvents, kafkaMessage);
    }
}
using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.Swipe.DTO;
using AmourLink.Swipe.KafkaMessages;
using AmourLink.Swipe.Services.Interfaces;
using AmourLink.Swipe.StaticConstants;
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
        var kafkaMessage = SwipeKafkaMessage.Like(swipeDto.SenderId, swipeDto.ReceiverId);

        await _producer.ProduceInternalAsync(TopicNames.SwipeEvents, kafkaMessage);
    }

    public async Task DislikeAsync(SwipeDto swipeDto)
    {
        var kafkaMessage = SwipeKafkaMessage.Dislike(swipeDto.SenderId, swipeDto.ReceiverId);

        await _producer.ProduceInternalAsync(TopicNames.SwipeEvents, kafkaMessage);
    }
}
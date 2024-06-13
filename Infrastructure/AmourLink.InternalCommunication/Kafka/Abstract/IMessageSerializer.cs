namespace AmourLink.InternalCommunication.Kafka.Abstract;

public interface IMessageSerializer<TSerialized>
{
    TSerialized Serialize<T>(T value);
    T Deserialize<T>(TSerialized value);
}
namespace AmourLink.InternalCommunication.Kafka.Options;

public class KafkaOptions
{
    public required string BootstrapServers { get; set; }
    public Dictionary<string, string> Consumer { get; set; } = [];
    public Dictionary<string, string> Producer { get; set; } = [];
}
using AmourLink.InternalCommunication.Kafka.Abstract;
using Newtonsoft.Json;

namespace AmourLink.InternalCommunication.Kafka;

public class JsonMessageSerializer : IMessageSerializer<string>
{
    public string Serialize<T>(T value)
    {
        return JsonConvert.SerializeObject(value);
    }

    public T Deserialize<T>(string value)
    {
        var result = JsonConvert.DeserializeObject<T>(value) ??
                     throw new NullReferenceException($"Deserialized object is null. Check the input JSON string");

        return result;
    }
}
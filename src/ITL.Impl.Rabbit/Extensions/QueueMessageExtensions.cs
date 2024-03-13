using System.Text.Json;
using System.Text.Json.Serialization;

namespace ITL.Impl.Rabbit.Extensions;

public static class QueueMessageExtensions
{
    public static byte[] ConvertToBody(this IQueueMessage message)
    {
        var jsonSerializerSettings = new JsonSerializerOptions()
        {
            MaxDepth = 5,
            UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
        };
        return JsonSerializer.SerializeToUtf8Bytes(message, jsonSerializerSettings);
    }

    public static IQueueMessage ConvertFromBody(this byte[] body)
    {
        return JsonSerializer.Deserialize<QueueMessageRabbitMQ>(body)!;
    }
}
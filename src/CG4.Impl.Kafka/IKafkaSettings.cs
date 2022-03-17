using System.Collections.Generic;

namespace Tolar.Kafka
{
    public interface IKafkaSettings
    {
        string Topic { get; }

        Dictionary<string, string> Config { get; }
    }
}
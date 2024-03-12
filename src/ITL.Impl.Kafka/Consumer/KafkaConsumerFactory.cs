using Microsoft.Extensions.Logging;

namespace ITL.Impl.Kafka.Consumer
{
    public class KafkaConsumerFactory : IKafkaConsumerFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IKafkaConsumerSettings _kafkaSettings;

        public KafkaConsumerFactory(ILoggerFactory loggerFactory, IKafkaConsumerSettings kafkaSettings)
        {
            _loggerFactory = loggerFactory;
            _kafkaSettings = kafkaSettings;
        }

        public IKafkaConsumer CreateConsumer(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentNullException(nameof(topic));
            }

            return new KafkaConsumer(_loggerFactory.CreateLogger<KafkaConsumer>(), _kafkaSettings, topic);
        }
    }
}

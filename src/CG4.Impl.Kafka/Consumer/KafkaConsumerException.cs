namespace CG4.Impl.Kafka.Consumer
{
    public class KafkaConsumerException : Exception
    {
        public KafkaConsumerException()
        {
        }

        public KafkaConsumerException(string message)
            : base(message)
        {
        }
    }
}

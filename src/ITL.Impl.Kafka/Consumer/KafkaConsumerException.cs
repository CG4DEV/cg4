namespace ITL.Impl.Kafka.Consumer
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

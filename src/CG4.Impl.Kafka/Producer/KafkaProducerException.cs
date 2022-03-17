namespace CG4.Impl.Kafka.Producer
{
    [Serializable]
    public class KafkaProducerException : Exception
    {
        public KafkaProducerException()
        {
        }

        public KafkaProducerException(string message)
            : base(message)
        {
        }
    }
}
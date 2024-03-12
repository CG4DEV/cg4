namespace ITL.Impl.Kafka.Consumer
{
    public interface IKafkaConsumerFactory
    {
        public IKafkaConsumer CreateConsumer(string topic);
    }
}

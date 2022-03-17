namespace CG4.Impl.Kafka.Consumer
{
    public interface IKafkaConsumerFactory
    {
        public IKafkaConsumer CreateConsumer(string topic);
    }
}

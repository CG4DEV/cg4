namespace ITL.Impl.Kafka.Producer
{
    public interface IKafkaProducer
    {
        public Task SendAsync<T>(T obj, string topic, object key = null);
    }
}

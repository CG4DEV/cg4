namespace CG4.Impl.Kafka.Producer
{
    public interface IKafkaProducer
    {
        public Task SendAsync<T>(T obj, string topic, object key = null);
    }
}

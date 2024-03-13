namespace ITL.Impl.Kafka.Consumer
{
    public delegate Task AsyncEventHandler<TEventArgs>(object? sender, TEventArgs args);

    public interface IKafkaConsumer
    {
        event AsyncEventHandler<MessageReceivedEventArgs> MessageReceived;

        event EventHandler ConsumerInitialized;

        event EventHandler KafkaError;

        void Start();

        void StartWithRollback(int rollbackInMinutes);

        void Stop();
    }
}

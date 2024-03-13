namespace ITL.Impl.Rabbit
{
    public delegate Task QueueMessageHandler(IQueueMessage message);

    public interface IQueueProvider : IDisposable
    {
        void PushMessage(IQueueMessage message);

        void PushMessage(IQueueMessage message, params string[] queues);

        uint GetQueueMessageCount(string queueName);
    }
}
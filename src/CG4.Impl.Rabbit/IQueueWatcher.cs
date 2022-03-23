namespace CG4.Impl.Rabbit
{
    public delegate Task QueueMessageReceive<in TEntity>(object queue, TEntity message)
        where TEntity : IQueueMessage;

    public interface IQueueWatcher<TQueueMessage>
    {
        event QueueMessageHandler Subscribe;

        event EventHandler<ExtThreadExceptionEventArgs> Error;

        void StartWatch(string queueName);
    }
}
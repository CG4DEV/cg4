namespace ITL.Impl.Rabbit
{
    public interface IQueueConsumer
    {
        event QueueMessageHandler Subscribe;

        event EventHandler<ExtThreadExceptionEventArgs> Error;

        void StartWatch(string queueName);
    }
}
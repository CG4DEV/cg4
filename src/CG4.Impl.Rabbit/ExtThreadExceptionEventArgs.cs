namespace CG4.Impl.Rabbit
{
    public class ExtThreadExceptionEventArgs : ThreadExceptionEventArgs
    {
        public ExtThreadExceptionEventArgs(IQueueMessage message, Exception exp)
            : base(exp)
        {
            Message = message;
        }

        public IQueueMessage Message { get; private set; }
    }
}

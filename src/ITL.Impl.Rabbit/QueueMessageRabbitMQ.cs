namespace ITL.Impl.Rabbit
{
    public class QueueMessageRabbitMQ : IQueueMessage
    {
        private const int DelayStep = 900000;

        public QueueMessageRabbitMQ()
        {
            Errors = new List<string>();
        }

        public QueueMessageRabbitMQ(string body) : this()
        {
            Body = body;
        }

        public Guid Uid { get; set; }

        public string Body { get; set; }

        public int? Delay
        {
            get
            {
                if (!Errors.Any())
                {
                    return 0;
                }

                return (1 << Errors.Count) * DelayStep;
            }

            set
            {
                // do nothing
            }
        }

        public IList<string> Errors { get; set; }
    }
}

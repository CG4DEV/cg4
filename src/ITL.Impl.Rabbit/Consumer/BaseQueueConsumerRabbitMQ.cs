using RabbitMQ.Client;

namespace ITL.Impl.Rabbit.Consumer
{
    public abstract class BaseQueueConsumerRabbitMQ<T>
        where T : IBasicConsumer
    {
        protected const ushort PREFETCH_COUNT = 50;

        protected readonly IConnectionFactory _connectionFactory;
        protected readonly ushort _prefetchCount;

        protected T _consumer;
        protected IConnection _connection;
        protected IModel _model;
        protected string _watchingQueueName;

        protected BaseQueueConsumerRabbitMQ(
            IConnectionFactory connectionFactory,
            ushort prefetchCount = PREFETCH_COUNT)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _prefetchCount = prefetchCount;

            Init();
        }

        public event QueueMessageHandler Subscribe;

        public event EventHandler<ExtThreadExceptionEventArgs> Error;

        public void StartWatch(string queueName)
        {
            if (Subscribe == null)
            {
                throw new ArgumentNullException(nameof(Subscribe));
            }

            _watchingQueueName = queueName;
            _consumer.Model.BasicConsume(_watchingQueueName, false, _consumer);
        }

        protected Task OnSubscribe(IQueueMessage message)
        {
            return Subscribe(message);
        }

        protected void OnError(object sender, ExtThreadExceptionEventArgs args)
        {
            Error?.Invoke(sender, args);
        }

        protected virtual void Init()
        {
            if (_model != null && _model.IsOpen)
            {
                _model.Close();
            }

            _model = null;

            if (_connection != null && _connection.IsOpen)
            {
                _connection.Close();
            }

            _connection = null;

            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            _model.BasicQos(0, _prefetchCount, false);

            InitConsumer();
        }

        protected abstract void InitConsumer();
    }
}

using CG4.Extensions;
using RabbitMQ.Client;

namespace CG4.Impl.Rabbit
{
    /// <summary>
    /// Желательное использование одного провайдера на поток
    /// </summary>
    public class QueueProviderRabbitMQ : IQueueProvider
    {
        private static readonly object _locker = new object();
        private static readonly object _lockChannel = new object();

        private bool _disposed;
        private readonly string _defaultExchange;

        private readonly bool _useDelay;
        private List<string> _queues;
        private IConnectionFactory _factory;
        private IMessageProvider _msgProvider;

        private IModel _channel;

        public QueueProviderRabbitMQ(
            IMessageProvider msgProvider,
            IConnectionFactory factory,
            bool useDelay = false,
            string defaultExchange = "CG4.direct",
            params string[] queues)
        {
            _msgProvider = msgProvider ?? throw new ArgumentNullException(nameof(msgProvider));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));

            if (queues.NullOrEmpty())
            {
                throw new ArgumentNullException(nameof(queues));
            }

            _useDelay = useDelay;
            _queues = queues.ToList();
            _defaultExchange = _useDelay ? $"{defaultExchange}.delay" : defaultExchange;

            Init(queues);
        }

        ~QueueProviderRabbitMQ()
        {
            Dispose(false);
        }

        public void PushMessage(IQueueMessage message)
        {
            PushMessageInternal(_queues, message);
        }

        public void PushMessage(IQueueMessage message, params string[] queues)
        {
            if (queues.Length == 0)
            {
                PushMessage(message);
            }

            queues = queues.Distinct().ToArray();
            RegisterMissQueues(queues);
            PushMessageInternal(queues, message);
        }

        public uint GetQueueMessageCount(string queueName)
        {
            var queue = _queues.SingleOrDefault(x => x.Equals(queueName, StringComparison.InvariantCultureIgnoreCase));
            if (queue == null)
            {
                throw new ArgumentException($"Не найдена очередь с именем {queueName}");
            }

            using (var channel = CreateChannel())
            {
                return channel.MessageCount(queue);
            }
        }

        public void Dispose()
        {
            Dispose(true);

            // подавляем финализацию
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing && _channel != null)
                {
                    // Освобождаем управляемые ресурсы
                    _channel.Close();
                    _channel.Dispose();
                }

                // освобождаем неуправляемые объекты
                _channel = null;
                _factory = null;
                _msgProvider = null;
                _queues = null;

                _disposed = true;
            }
        }

        private void RegisterMissQueues(IEnumerable<string> queues)
        {
            lock (_locker)
            {
                var queuesToSend = queues.Except(_queues).ToArray();
                if (queuesToSend.Length > 0)
                {
                    Init(queuesToSend);
                    _queues.AddRange(queuesToSend);
                }
            }
        }

        private void PushMessageInternal(IEnumerable<string> queues, IQueueMessage message)
        {
            var channel = CreateChannel();
            var body = _msgProvider.PrepareMessageByte(message);

            foreach (var item in queues)
            {
                var bProp = channel.CreateBasicProperties();
                var bHeaders = new Dictionary<string, object>();

                bHeaders.Add("x-delay", message.Delay);
                bProp.Headers = bHeaders;
                bProp.DeliveryMode = 2;

                channel.BasicPublish(_defaultExchange, item, bProp, body);
            }
        }

        /// <summary>
        /// Проверяем что очередь создана.
        /// </summary>
        private void Init(IEnumerable<string> queues)
        {
            using (var channel = CreateChannel())
            {
                foreach (var item in queues)
                {
                    if (_useDelay)
                    {
                        // при подключении плагина на задержку времени
                        var args = new Dictionary<string, object> { { "x-delayed-type", "direct" } };
                        channel.ExchangeDeclare(_defaultExchange, "x-delayed-message", true, false, args);
                    }
                    else
                    {
                        // без плагина
                        channel.ExchangeDeclare(_defaultExchange, "direct", true, false, null);
                    }

                    var queue = channel.QueueDeclare(item, true, false, false, null);
                    channel.QueueBind(queue, _defaultExchange, item);
                }
            }
        }

        private IModel CreateChannel()
        {
            if (_channel is not { IsOpen: true })
            {
                //Лочим на всякий, вдруг один экземпляр провайдера попадет в несколько потоков
                lock (_lockChannel)
                {
                    if (_channel is not { IsOpen: true })
                    {
                        _channel = _factory.CreateConnection().CreateModel();
                    }
                }
            }

            return _channel;
        }
    }
}
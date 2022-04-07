using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CG4.Impl.Rabbit.Consumer
{
    public class QueueConsumerAsyncRabbitMQ : BaseQueueConsumerRabbitMQ<AsyncEventingBasicConsumer>, IQueueWatcher<IQueueMessage>
    {
        public QueueConsumerAsyncRabbitMQ(IConnectionFactory connectionFactory, IMessageProvider provider)
            : this(connectionFactory, provider, PREFETCH_COUNT)
        {
        }

        public QueueConsumerAsyncRabbitMQ(
            IConnectionFactory connectionFactory,
            IMessageProvider provider,
            ushort prefetchCount)
            : base(connectionFactory, provider, prefetchCount)
        {
        }

        protected override void InitConsumer()
        {
            if (_consumer != null)
            {
                _consumer.Received -= ReceiverHandler;
                _consumer.Shutdown -= ShutdownHandler;
            }

            _consumer = new AsyncEventingBasicConsumer(_model);
            _consumer.Received += ReceiverHandler;
            _consumer.Shutdown += ShutdownHandler;
        }

        private async Task ReceiverHandler(object sender, BasicDeliverEventArgs e)
        {
            var message = _provider.ExtractObject(Encoding.UTF8.GetString(e.Body.ToArray()));
            try
            {
                await OnSubscribe(message);
            }
            catch (Exception ex)
            {
                OnError(this, new ExtThreadExceptionEventArgs(message, ex));
            }
            finally
            {
                var model = ((AsyncEventingBasicConsumer)sender).Model;
                model.BasicAck(e.DeliveryTag, false);
            }
        }

        private Task ShutdownHandler(object sender, ShutdownEventArgs e)
        {
            InitConsumer();
            StartWatch(_watchingQueueName);

            return Task.CompletedTask;
        }
    }
}
using ITL.Impl.Rabbit.Extensions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ITL.Impl.Rabbit.Consumer
{
    public class QueueConsumerRabbitMQ : BaseQueueConsumerRabbitMQ<EventingBasicConsumer>, IQueueConsumer
    {
        public QueueConsumerRabbitMQ(
            IConnectionFactory connectionFactory,
            ushort prefetchCount = PREFETCH_COUNT)
            : base(connectionFactory, prefetchCount)
        {
        }

        protected override void InitConsumer()
        {
            if (_consumer != null)
            {
                _consumer.Received -= ReceiverHandler;
                _consumer.Shutdown -= ShutdownHandler;
            }

            _consumer = new EventingBasicConsumer(_model);
            _consumer.Received += ReceiverHandler;
            _consumer.Shutdown += ShutdownHandler;
        }

        private void ReceiverHandler(object sender, BasicDeliverEventArgs e)
        {
            var message = e.Body.ToArray().ConvertFromBody();
            try
            {
                OnSubscribe(message).Wait();
            }
            catch (Exception ex)
            {
                OnError(this, new ExtThreadExceptionEventArgs(message, ex));
            }
            finally
            {
                var model = ((EventingBasicConsumer)sender).Model;
                model.BasicAck(e.DeliveryTag, false);
            }
        }

        private void ShutdownHandler(object sender, ShutdownEventArgs e)
        {
            InitConsumer();
            StartWatch(_watchingQueueName);
        }
    }
}
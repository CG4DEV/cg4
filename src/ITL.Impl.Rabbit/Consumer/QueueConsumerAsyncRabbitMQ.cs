using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ITL.Impl.Rabbit.Extensions;

namespace ITL.Impl.Rabbit.Consumer
{
    public class QueueConsumerAsyncRabbitMQ : BaseQueueConsumerRabbitMQ<AsyncEventingBasicConsumer>, IQueueConsumer
    {
        public QueueConsumerAsyncRabbitMQ(
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

            _consumer = new AsyncEventingBasicConsumer(_model);
            _consumer.Received += ReceiverHandler;
            _consumer.Shutdown += ShutdownHandler;
        }

        private async Task ReceiverHandler(object sender, BasicDeliverEventArgs e)
        {
            var message = e.Body.ToArray().ConvertFromBody();
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
                await Task.Yield();
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
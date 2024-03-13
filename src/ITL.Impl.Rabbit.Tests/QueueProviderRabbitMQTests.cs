using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RabbitMQ.Client;
using Xunit;

namespace ITL.Impl.Rabbit.Tests;

public class QueueProviderRabbitMQTests
{
    [Fact]
    public void CreateQueueProviderInstance_WithProviderSettingsConstructor_Correct()
    {
        var model = new Mock<IModel>();
        var connectionFactory = MockRabbitObjects.GetFactory(model, "queue1");
        IQueueProviderSettings settings = new MockQueueProviderSettings();
        var queueProviderRabbitMQ = new QueueProviderRabbitMQ(connectionFactory, settings);
        Assert.NotNull(queueProviderRabbitMQ);
    }
    
    [Fact]
    public void CreateQueueProviderInstance_WithParamsConstructor_Correct()
    {
        var model = new Mock<IModel>();
        var connectionFactory = MockRabbitObjects.GetFactory(model, "queue1");
        var queueProviderRabbitMQ = new QueueProviderRabbitMQ(connectionFactory, true, "my-exchange", "queue1");
        Assert.NotNull(queueProviderRabbitMQ);
    }

    
    [Fact]
    public void PushMessageInExistQueue_WithDelayAndCustomExchange_CorrectSent()
    {
        var model = new Mock<IModel>();
        var connectionFactory = MockRabbitObjects.GetFactory(model, "queue1");
        var queueProviderRabbitMQ = new QueueProviderRabbitMQ(connectionFactory, true, "my-exchange", "queue1");
        var testMessage = MockRabbitObjects.GetCorrectMessage();
        queueProviderRabbitMQ.PushMessage(testMessage);
        model.Verify(x => x.BasicPublish("my-exchange.delay", "queue1", false, It.IsAny<IBasicProperties>(), It.IsAny<ReadOnlyMemory<byte>>()));
    }

    [Fact]
    public void PushMessageInQueues_WithDelayAndCustomExchange_CorrectSent()
    {
        var model = new Mock<IModel>();
        model.Setup(x => x.QueueDeclare("queue2", It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), null))
            .Returns(new QueueDeclareOk("queue2", 12, 2));
        var connectionFactory = MockRabbitObjects.GetFactory(model, "queue1");
        var queueProviderRabbitMQ = new QueueProviderRabbitMQ(connectionFactory, true, "my-exchange", new[] { "queue2" });
        var testMessage = MockRabbitObjects.GetCorrectMessage();
        queueProviderRabbitMQ.PushMessage(testMessage, "queue1", "queue2");
        model.Verify(x => x.BasicPublish("my-exchange.delay", "queue1", false, It.IsAny<IBasicProperties>(), It.IsAny<ReadOnlyMemory<byte>>()));
        model.Verify(x => x.BasicPublish("my-exchange.delay", "queue2", false, It.IsAny<IBasicProperties>(), It.IsAny<ReadOnlyMemory<byte>>()));
    }
    
    [Fact]
    public void GetQueueMessageCount_EqualsQueue_ReturnCorrectMessageCount()
    {
        var queueName = "queue1";
        var model = new Mock<IModel>();
        model.Setup(x => x.MessageCount(queueName))
            .Returns(10);
        
        var connectionFactory = MockRabbitObjects.GetFactory(model, "queue1");
        var queueProviderRabbitMQ = new QueueProviderRabbitMQ(connectionFactory, true, queues: new[] { queueName });
        var count = queueProviderRabbitMQ.GetQueueMessageCount(queueName);
        Assert.Equal((uint)10, count);
    }
    
    [Fact]
    public void GetQueueMessageCount_DifferentQueues_ThrowArgumentNullException()
    {
        var queueName = "queue1";
        var model = new Mock<IModel>();
        model.Setup(x => x.MessageCount(queueName))
            .Returns(10);
        
        var connectionFactory = MockRabbitObjects.GetFactory(model, "queue1");
        var queueProviderRabbitMQ = new QueueProviderRabbitMQ(connectionFactory, true, queues: new[] { queueName });
        var result = Assert.Throws<ArgumentNullException>(() => queueProviderRabbitMQ.GetQueueMessageCount("new queue"));
        Assert.NotNull(result);
    }
    
    private class MockQueueProviderSettings : IQueueProviderSettings
    {
        public string DefaultExchange { get; set; } = "ITL.direct.test";
        public bool UseDelay { get; set; } = false;
        public string[] Queues { get; set; } = { "queue1" };
        public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Transient;
    }
}
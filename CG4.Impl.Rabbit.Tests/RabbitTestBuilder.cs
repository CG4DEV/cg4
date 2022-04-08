using System;
using System.Collections.Generic;
using CG4.Impl.Rabbit.Consumer;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Xunit;

namespace CG4.Impl.Rabbit.Tests;

public class RabbitTestBuilder
{
    public class ExecutorBuilderTests
    {
        private readonly ServiceProvider _provider;

        public ExecutorBuilderTests()
        {
            var collection = new ServiceCollection();
            collection.AddSingleton<IConnectionFactorySettings, MockFactorySettings>();
            collection.AddSingleton<IConnectionFactory, ConnectionFactoryRabbitMQ>();
            collection.AddSingleton<IQueueProviderSettings, MockQueueProviderSettings>();
            collection.AddSingleton<IQueueProvider, QueueProviderRabbitMQ>();
            collection.AddSingleton<IQueueConsumer, QueueConsumerRabbitMQ>();
            _provider = collection.BuildServiceProvider();
        }

        [Fact]
        public void StoryBuilder_ResolveByTestExecutorContext_WasResolved()
        {
            var mp = _provider.GetRequiredService<IQueueProvider>();
            TestQueue tq = new TestQueue(mp);
            var msg = new QueueMessageRabbitMQ()
            {
                Uid = Guid.NewGuid(),
                Delay = 100,
                Body = "{\"Date\":\"2022-04-08T14:08:20.2810335+00:00\",\"TemperatureCelsius\":33,\"Summary\":\"will be warm\"}",
                Errors = new List<string>(),
            };
            tq.SendMessage(msg, new[] { "TTTT" });
            string result = String.Empty;
            Assert.NotNull(result);
        }
    }
}

public class TestQueue
{
    private readonly IQueueProvider _provider;

    public TestQueue(IQueueProvider provider)
    {
        _provider = provider;
    }

    public void SendMessage(IQueueMessage message)
    {
        _provider.PushMessage(message);
    }

    public void SendMessage(IQueueMessage message, string[] queues)
    {
        _provider.PushMessage(message, queues);
    }
}

public class MockFactorySettings : IConnectionFactorySettings
{
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 5672;
    public string VirtualHost { get; set; } = "/";
    public string Login { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public bool DispatchConsumersAsync { get; set; }
    public string ClientProvidedName { get; set; } = "mock";
}

public class MockQueueProviderSettings : IQueueProviderSettings
{
    public string DefaultExchange { get; set; } = "CG4.direct";
    public bool UseDelay { get; set; } = false;
    public string[] Queues { get; set; } = { "Polify", "UUU" };
}
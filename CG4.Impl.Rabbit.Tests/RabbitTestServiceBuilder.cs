using CG4.Impl.Rabbit.Consumer;
using CG4.Impl.Rabbit.Extensions;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Xunit;

namespace CG4.Impl.Rabbit.Tests;

public class RabbitTestServiceBuilder
{
    [Fact]
    public void RegisterRabbitServices_WithoutExtensions_WasResolved()
    {
        var collection = new ServiceCollection();
        collection.AddSingleton<IConnectionFactorySettings, MockFactorySettings>();
        collection.AddSingleton<IConnectionFactory, ConnectionFactoryRabbitMQ>();
        collection.AddSingleton<IQueueProviderSettings, MockQueueProviderSettings>();
        collection.AddSingleton<IQueueProvider, QueueProviderRabbitMQ>();
        collection.AddSingleton<IQueueConsumer, QueueConsumerRabbitMQ>();

        var serviceProvider = collection.BuildServiceProvider();

        var factorySettings = serviceProvider.GetRequiredService<IConnectionFactorySettings>();
        var connectionFactory = serviceProvider.GetRequiredService<IConnectionFactory>();
        var providerSettings = serviceProvider.GetRequiredService<IQueueProviderSettings>();
        var provider = serviceProvider.GetRequiredService<IQueueProvider>();
        var consumer = serviceProvider.GetRequiredService<IQueueConsumer>();

        Assert.NotNull(factorySettings);
        Assert.NotNull(connectionFactory);
        Assert.NotNull(providerSettings);
        Assert.NotNull(provider);
        Assert.NotNull(consumer);
        Assert.Equal("localhost", factorySettings.Host);
    }

    [Fact]
    public void RegisterRabbitServices_WithExtensions_WasResolved()
    {
        var collection = new ServiceCollection();

        collection.AddCG4RabbitBasicServices(settings =>
        {
            settings.FactorySettings = new MockFactorySettings();
            settings.ProviderSettings = new MockQueueProviderSettings();
        });
        var serviceProvider = collection.BuildServiceProvider();
        
        var factorySettings = serviceProvider.GetRequiredService<IConnectionFactorySettings>();
        var connectionFactory = serviceProvider.GetRequiredService<IConnectionFactory>();
        var providerSettings = serviceProvider.GetRequiredService<IQueueProviderSettings>();
        var provider = serviceProvider.GetRequiredService<IQueueProvider>();
        var consumer = serviceProvider.GetRequiredService<IQueueConsumer>();

        Assert.NotNull(factorySettings);
        Assert.NotNull(connectionFactory);
        Assert.NotNull(providerSettings);
        Assert.NotNull(provider);
        Assert.NotNull(consumer);
        Assert.Equal("localhost", factorySettings.Host);
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
    public string DefaultExchange { get; set; } = "CG4.direct.test";
    public bool UseDelay { get; set; } = false;
    public string[] Queues { get; set; } = { "test1", "test2" };
    public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Transient;
}
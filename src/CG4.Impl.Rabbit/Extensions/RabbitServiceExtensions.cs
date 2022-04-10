using CG4.Impl.Rabbit.Consumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RabbitMQ.Client;

namespace CG4.Impl.Rabbit.Extensions;

/// <summary>
/// Extensions for registers basic rabbit MQ services.
/// </summary>
public static class RabbitServiceExtensions
{
    /// <summary>
    /// Registers all services for easy start to work with Rabbit.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configSettings">The settings <see cref="RabbitConfigSettings"/>.</param>
    /// <remarks>If you want to use custom rabbit services, resolve it manually.</remarks>
    public static void AddCG4RabbitBasicServices(this IServiceCollection services, Action<RabbitConfigSettings> configSettings)
    {
        if (configSettings == null)
        {
            throw new ArgumentNullException(nameof(configSettings));
        }

        var settings = new RabbitConfigSettings();
        configSettings.Invoke(settings);

        if (settings.FactorySettings == null)
        {
            throw new ArgumentNullException(nameof(settings.FactorySettings));
        }

        if (settings.ProviderSettings == null)
        {
            throw new ArgumentNullException(nameof(settings.ProviderSettings));
        }

        services.AddSingleton(settings.FactorySettings);
        services.AddSingleton<IConnectionFactory, ConnectionFactoryRabbitMQ>();

        services.AddSingleton(settings.ProviderSettings);
        services.TryAdd(new ServiceDescriptor(typeof(IQueueProvider), typeof(QueueProviderRabbitMQ), settings.ProviderSettings.Lifetime));

        if (settings.FactorySettings.DispatchConsumersAsync)
        {
            services.AddSingleton<IQueueConsumer, QueueConsumerAsyncRabbitMQ>();
        }
        else
        {
            services.AddSingleton<IQueueConsumer, QueueConsumerRabbitMQ>();
        }
    }
}
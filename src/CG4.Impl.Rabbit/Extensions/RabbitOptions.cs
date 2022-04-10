namespace CG4.Impl.Rabbit.Extensions;

/// <summary>
/// Configuration for rabbit services settings.
/// </summary>
public class RabbitConfigSettings
{
    /// <summary>
    /// Settings of <see cref="IConnectionFactorySettings"/> that inject for example: <see cref="ConnectionFactoryRabbitMQ"/>
    /// </summary>
    public IConnectionFactorySettings FactorySettings { get; set; }

    /// <summary>
    /// Settings for <see cref="IQueueProvider"/> that inject for example: <see cref="QueueProviderRabbitMQ"/>
    /// </summary>
    public IQueueProviderSettings ProviderSettings { get; set; }
}
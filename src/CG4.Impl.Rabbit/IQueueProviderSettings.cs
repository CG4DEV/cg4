using Microsoft.Extensions.DependencyInjection;

namespace CG4.Impl.Rabbit;

/// <summary>
/// interface for provider settings.
/// </summary>
public interface IQueueProviderSettings
{
    /// <summary>
    /// Default exchange for provider.
    /// </summary>
    string DefaultExchange { get; set; }
    
    /// <summary>
    /// Use delay for send messaging.
    /// </summary>
    bool UseDelay { get; set; }
    
    /// <summary>
    /// Default queues for provider.
    /// </summary>
    string[] Queues { get; set; }

    /// <summary>
    /// Life time of provider.
    /// </summary>
    ServiceLifetime Lifetime { get; set; }
}
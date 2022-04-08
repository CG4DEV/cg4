namespace CG4.Impl.Rabbit;

public interface IQueueProviderSettings
{
    string DefaultExchange { get; set; }
    
    bool UseDelay { get; set; }
    
    string[] Queues { get; set; }
}
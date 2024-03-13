namespace ITL.Impl.Kafka.Consumer
{
    public interface IKafkaConsumerSettings
    {
        int MaxTimeoutMsec { get; set; }

        int MaxThreadsCount { get; set; }

        Dictionary<string, string> Config { get; set; }
    }
}

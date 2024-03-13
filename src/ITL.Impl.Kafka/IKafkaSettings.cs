namespace ITL.Impl.Kafka
{
    public interface IKafkaSettings
    {
        string Topic { get; }

        Dictionary<string, string> Config { get; }
    }
}
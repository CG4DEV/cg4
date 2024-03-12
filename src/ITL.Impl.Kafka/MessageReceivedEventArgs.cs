namespace ITL.Impl.Kafka
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public int Partition { get; }

        public long Offset { get; }

        public string Key { get; }

        public string Data { get; }

        public DateTime Timestamp { get; }

        public MessageReceivedEventArgs(int partition, long offset, string key, string data, DateTime timestamp)
        {
            Partition = partition;
            Offset = offset;
            Data = data;
            Timestamp = timestamp;
            Key = key;
        }

        public override string ToString()
        {
            return $"Offset: {Offset}, Data: {Data}";
        }
    }
}

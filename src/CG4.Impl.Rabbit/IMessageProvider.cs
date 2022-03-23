namespace CG4.Impl.Rabbit
{
    public interface IMessageProvider
    {
        string PrepareMessageStr(object source);

        byte[] PrepareMessageByte(object source);

        IQueueMessage ExtractObject(string source);

        IQueueMessage ExtractObject(byte[] source);
    }
}
namespace QueueSDK.Interfaces
{
    public interface INotifier<T>
    {
        void SendToQueue(T payload);
    }
}

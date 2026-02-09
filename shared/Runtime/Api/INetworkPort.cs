namespace PvpRush.Shared.Api
{
    public interface INetworkPort
    {
        void SendMessageAndForget<T>(T message) where T : INetworkMessageBufferWriter, INetworkMessageTypeProvider;
        void SendMessageAndEnsure<T>(T message) where T : INetworkMessageBufferWriter, INetworkMessageTypeProvider;
    }
}

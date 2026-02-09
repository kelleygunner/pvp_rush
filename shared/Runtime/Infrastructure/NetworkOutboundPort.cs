using PvpRush.Shared.Api;

namespace PvpRush.Shared.Infrastructure
{
    internal sealed class NetworkOutboundPort : INetworkPort
    {
        private readonly PacketBufferScope _bufferScope;

        public NetworkOutboundPort(IMessageSender messageSender)
        {
            _bufferScope = new PacketBufferScope(messageSender);
        }

        void INetworkPort.SendMessageAndForget<T>(T message)
        {
            _bufferScope.SendInstant(message);
        }
    }
}
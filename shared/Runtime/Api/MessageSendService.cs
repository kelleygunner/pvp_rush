using PvpRush.Shared.Infrastructure;

namespace PvpRush.Shared.Api
{
    public sealed class MessageSendService
    {
        private readonly PacketBufferScope _bufferScope;

        public MessageSendService(IMessageSender messageSender)
        {
            _bufferScope = new PacketBufferScope(messageSender);
        }

        public void SendMessage<T>(T message) where T : INetworkMessageBufferWriter, INetworkMessageTypeProvider
        {
            _bufferScope.SendInstant(message);
        }
    }
}

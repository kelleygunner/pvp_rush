using System;
using System.Buffers.Binary;
using PvpRush.Shared.Api.ValueTypes;

namespace PvpRush.Shared.Api.Messages
{
    public readonly struct PingMessage : INetworkMessageBufferWriter, INetworkMessageTypeProvider
    {
        private readonly uint _clientTime;
        private readonly bool _initialized;
        private const ushort PayloadSize = 4;
        
        public MessageType MsgType => MessageType.Ping;
        public int PacketExpectedSize => PayloadSize + ConnectionConfig.HeaderSize;

        public PingMessage(uint clientTimeMs)
        {
            _clientTime = clientTimeMs;
            _initialized = true;
        }
        
        public int TryCreateMessage(Span<byte> buffer)
        {
            if (!_initialized)
            {
                return 0;
            }

            // Write Header
            var headerSpan = buffer.Slice(0, ConnectionConfig.HeaderSize);
            if (!MsgType.TryCreateHeader(PayloadSize, headerSpan))
            {
                return 0;
            }

            // Write Payload
            var clientTimeSpan = buffer.Slice(ConnectionConfig.HeaderSize, 4);
            BinaryPrimitives.WriteUInt32LittleEndian(clientTimeSpan, _clientTime);
            
            return PacketExpectedSize;
        }
    }
}
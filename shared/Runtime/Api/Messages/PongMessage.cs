using System;
using System.Buffers.Binary;
using PvpRush.Shared.Api.ValueTypes;

namespace PvpRush.Shared.Api.Messages
{
    public readonly struct PongMessage : INetworkMessageBufferWriter, INetworkMessageTypeProvider
    {
        private readonly uint _clientTime;
        private readonly uint _serverTime;
        private readonly bool _initialized;
        private const ushort PayloadSize = 8;

        public MessageType MsgType => MessageType.Pong;

        public int PacketExpectedSize => ConnectionConfig.HeaderSize + PayloadSize;
        
        public PongMessage(uint clientTimeMs, uint serverTimeMs)
        {
            _clientTime = clientTimeMs;
            _serverTime = serverTimeMs;
            _initialized = true;
        }

        public int TryCreateMessage(Span<byte> buffer)
        {
            if (!_initialized)
            {
                return 0;
            }
            
            //buffer = new byte[ConnectionConfig.HeaderSize + PayloadSize];

            // Write Header
            var headerSpan = buffer.Slice(0, ConnectionConfig.HeaderSize);
            if (!MsgType.TryCreateHeader(PayloadSize, headerSpan))
            {
                return 0;
            }

            // Write Payload
            var clientTimeSpan = buffer.Slice(ConnectionConfig.HeaderSize, 4);
            var serverTimeSpan = buffer.Slice(ConnectionConfig.HeaderSize + 4, 4);
            BinaryPrimitives.WriteUInt32LittleEndian(clientTimeSpan, _clientTime);
            BinaryPrimitives.WriteUInt32LittleEndian(serverTimeSpan, _serverTime);
            
            return ConnectionConfig.HeaderSize + PayloadSize;
        }
    }
}
using System;
using System.Buffers.Binary;
using PvpRush.Shared.Network;

// ReSharper disable once CheckNamespace
namespace PvpRush.Shared.Messages
{
    public readonly struct PongMessage : INetworkMessageWriter, INetworkMessageTypeProvider
    {
        private readonly uint _clientTime;
        private readonly uint _serverTime;
        private readonly bool _initialized;
        private const ushort PayloadSize = 8;

        public MessageType MsgType => MessageType.Pong;
        
        public PongMessage(uint clientTimeMs, uint serverTimeMs)
        {
            _clientTime = clientTimeMs;
            _serverTime = serverTimeMs;
            _initialized = true;
        }

        public byte[] CreateMessage()
        {
            if (!_initialized)
                throw new InvalidOperationException("Message not initialized");
            
            var bytes = new byte[ConnectionConfig.HeaderSize + PayloadSize];

            // Write Header
            MsgType.CreateHeader(PayloadSize, bytes);

            // Write Payload
            var clientTimeSpan = bytes.AsSpan(ConnectionConfig.HeaderSize, 4);
            var serverTimeSpan = bytes.AsSpan(ConnectionConfig.HeaderSize + 4, 4);
            BinaryPrimitives.WriteUInt32LittleEndian(clientTimeSpan, _clientTime);
            BinaryPrimitives.WriteUInt32LittleEndian(serverTimeSpan, _serverTime);

            return bytes;
        }
    }
}
using System;
using System.Buffers.Binary;
using PvpRush.Shared.Network;

namespace PvpRush.Shared.Messages
{
    public readonly struct PingMessage : INetworkMessageWriter, INetworkMessageTypeProvider
    {
        private readonly uint _clientTime;
        private readonly bool _initialized;
        private const ushort PayloadSize = 4;

        public PingMessage(uint clientTimeMs)
        {
            _clientTime = clientTimeMs;
            _initialized = true;
        }

        public MessageType MsgType => MessageType.Ping;

        byte[] INetworkMessageWriter.CreateMessage()
        {
            if (!_initialized)
                throw new InvalidOperationException("Message not initialized");

            var version = ConnectionConfig.SharedLogicVersion;

            var messageType = (byte)MsgType;
            var size = ConnectionConfig.HeaderSize + PayloadSize;
            var bytes = new byte[size];

            bytes[ConnectionConfig.VersionByte] = version;
            bytes[ConnectionConfig.MessageTypeByte] = messageType;

            // Write Payload Size
            var payloadSizeSpan = bytes.AsSpan(ConnectionConfig.PayloadSizeStartByte,
                ConnectionConfig.PayloadSizeLength);
            BinaryPrimitives.WriteUInt16LittleEndian(payloadSizeSpan, PayloadSize);

            // Write Payload
            var payloadSpan = bytes.AsSpan(ConnectionConfig.HeaderSize, 4);
            BinaryPrimitives.WriteUInt32LittleEndian(payloadSpan, _clientTime);

            return bytes;
        }
    }

    public static class NetworkMessageUtils
    {
        public static void CreateHeader(MessageType type, ushort payloadSize, Span<byte> header)
        {
            // Write Version and Message Type
            var version = ConnectionConfig.SharedLogicVersion;
            var messageType = (byte)type;
            header[ConnectionConfig.VersionByte] = version;
            header[ConnectionConfig.MessageTypeByte] = messageType;

            // Write Payload Size
            var payloadSizeSpan = header.Slice(ConnectionConfig.PayloadSizeStartByte,
                ConnectionConfig.PayloadSizeLength);
            BinaryPrimitives.WriteUInt16LittleEndian(payloadSizeSpan, payloadSize);
        }
        
        public static void CreateHeader(this MessageType type, ushort payloadSize, byte[] header)
        {
            CreateHeader(type, payloadSize, header.AsSpan());
        }
    }
}
using System;
using System.Buffers.Binary;
using PvpRush.Shared.Api.ValueTypes;

namespace PvpRush.Shared.Api.Messages
{
    public static class NetworkMessageUtils
    {
        public static bool TryCreateHeader(this MessageType type, ushort payloadSize, Span<byte> header)
        {
            if (header.Length < ConnectionConfig.HeaderSize)
            {
                return false;
            }

            // Write Version and Message Type
            var version = ConnectionConfig.SharedLogicVersion;
            var messageType = (byte)type;
            header[ConnectionConfig.VersionByte] = version;
            header[ConnectionConfig.MessageTypeByte] = messageType;

            // Write Payload Size
            var payloadSizeSpan = header.Slice(ConnectionConfig.PayloadSizeStartByte,
                ConnectionConfig.PayloadSizeLength);
            BinaryPrimitives.WriteUInt16LittleEndian(payloadSizeSpan, payloadSize);
            return true;
        }
    }
}
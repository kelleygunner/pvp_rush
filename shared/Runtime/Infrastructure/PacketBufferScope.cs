using System;
using System.Buffers;
using PvpRush.Shared.Api;

namespace PvpRush.Shared.Infrastructure
{
    internal class PacketBufferScope
    {
        private const int BufferMaxSize = 512;
        private readonly IMessageSender _messageSender;

        public PacketBufferScope(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        internal bool SendInstant<T>(T messageWriter) where T : INetworkMessageBufferWriter
        {
            if (messageWriter.PacketExpectedSize < BufferMaxSize)
                return SendInstantWithStackalloc(messageWriter);
            return SendInstantByRentingBuffer(messageWriter);
        }

        private bool SendInstantWithStackalloc<T>(T messageWriter) where T : INetworkMessageBufferWriter
        {
            Span<byte> buffer = stackalloc byte[messageWriter.PacketExpectedSize];
            var size = messageWriter.TryCreateMessage(buffer);
            if (size == 0)
                return false;
            
            return _messageSender.TrySendMessage(buffer.Slice(0, size));
        }

        private bool SendInstantByRentingBuffer<T>(T messageWriter) where T : INetworkMessageBufferWriter
        {
            var buffer = RentBuffer(messageWriter.PacketExpectedSize);
            try
            {
                Span<byte> bufferSpan = buffer;
                var size = messageWriter.TryCreateMessage(bufferSpan);
                if (size == 0)
                {
                    return false;
                }

                return _messageSender.TrySendMessage(bufferSpan.Slice(0, size));
            }
            finally
            {
                ReturnBuffer(buffer);
            }
        }

        private byte[] RentBuffer(int size)
        {
            return ArrayPool<byte>.Shared.Rent(size);
        }

        private void ReturnBuffer(byte[] byteArray)
        {
            ArrayPool<byte>.Shared.Return(byteArray);
        }
    }
}
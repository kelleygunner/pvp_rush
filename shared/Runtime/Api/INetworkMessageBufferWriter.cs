using System;

namespace PvpRush.Shared.Api
{
    public interface INetworkMessageBufferWriter
    {
        int TryCreateMessage(Span<byte> buffer);
        int PacketExpectedSize { get; }
    }
}
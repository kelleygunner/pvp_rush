using System;

namespace PvpRush.Shared.Api
{
    public interface IMessageSender
    {
        bool TrySendMessage(ReadOnlySpan<byte> buffer);
    }
}
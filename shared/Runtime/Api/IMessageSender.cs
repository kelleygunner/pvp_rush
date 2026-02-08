using System;

namespace PvpRush.Shared.Api
{
    public interface IMessageSender
    {
        bool TrySendMessage(Span<byte> buffer);
    }
}
using PvpRush.Shared.Network;

// ReSharper disable once CheckNamespace
namespace PvpRush.Shared.Messages
{
    public interface INetworkMessageWriter
    {
        byte[] CreateMessage();
    }

    public interface INetworkMessageTypeProvider
    {
        MessageType MsgType { get; }
    }
}
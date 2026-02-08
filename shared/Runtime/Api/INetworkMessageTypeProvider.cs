using PvpRush.Shared.Api.ValueTypes;

namespace PvpRush.Shared.Api
{
    public interface INetworkMessageTypeProvider
    {
        MessageType MsgType { get; }
    }
}
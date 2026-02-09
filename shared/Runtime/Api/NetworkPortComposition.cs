using PvpRush.Shared.Infrastructure;

namespace PvpRush.Shared.Api
{
    public static class NetworkPortComposition
    {
        public static INetworkPort Create(IMessageSender messageSender)
        {
            return new NetworkOutboundPort(messageSender);
        }
    }
}
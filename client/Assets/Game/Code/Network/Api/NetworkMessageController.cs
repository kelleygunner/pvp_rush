using Game.Network.Infrastructure;
using PvpRush.Shared.Api;
using PvpRush.Shared.Api.Messages;

// ReSharper disable once CheckNamespace
namespace Game.Network.Api
{
    public class NetworkMessageController
    {
        private readonly MessageSendService _messageSendService;

        public NetworkMessageController()
        {
            var networkMessageTransport = new NetworkMessageTransport();
            _messageSendService = new MessageSendService(networkMessageTransport);
        }
        
        public void Ping()
        {
            var message = new PingMessage(124777);
            _messageSendService.SendMessage(message);
        }
    }
}
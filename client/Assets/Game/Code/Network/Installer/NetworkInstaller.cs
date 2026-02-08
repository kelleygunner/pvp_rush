using Game.Network.Api;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Game.Network.Installer
{
    public class NetworkInstaller : MonoBehaviour
    {
        private NetworkMessageController _networkMessageController;
        void Start()
        {
            _networkMessageController = new NetworkMessageController();
            _networkMessageController.Ping();
        }
    }
}

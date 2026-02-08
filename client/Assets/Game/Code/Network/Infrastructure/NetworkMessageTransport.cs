using System;
using PvpRush.Shared.Api;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Game.Network.Infrastructure
{
    public class NetworkMessageTransport : IMessageSender
    {
        public bool TrySendMessage(Span<byte> buffer)
        {
            Debug.Log($"NetworkMessageTransport.TrySendMessage(buffer)");
            return true;
        }
    }
}

using RR.WebsocketService_V1;
using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace RR.Logger_V1.LoggerServer
{
    public class NotificationsMessageHandler : WebSocketHandler
    {
        public NotificationsMessageHandler(WebSocketConnectionService webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public override Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            Debug.WriteLine("ReceiveAsync !!!");

            return null;
        }
    }
}

using Microsoft.AspNetCore.SignalR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.GameServices.Hubs
{
    public class LobbyHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Client connected: " + Context.ConnectionId);
            Console.WriteLine("User Id: " + Context.UserIdentifier);    
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Client disconnected: " + Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}

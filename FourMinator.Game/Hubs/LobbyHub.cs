using FourMinator.GameServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.GameServices.Hubs
{
    [Authorize]
    public class LobbyHub : Hub
    {

        private readonly ILobbyService _lobbyService;

        public LobbyHub(ILobbyService lobbyService) 
        {
            _lobbyService = lobbyService;
        }


        public async Task GetWaitingPlayers()
        {
            var players = await _lobbyService.GetWaitingPlayers();
            await Clients.All.SendAsync("ReceiveWaitingPlayers", players);
        }

        public override Task OnConnectedAsync()
        {
            
            Console.WriteLine("Client connected: " + Context.ConnectionId);
            
            _lobbyService.SetConnectingPlayerOnline(Context.UserIdentifier).Wait();
            GetWaitingPlayers().Wait();

            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Client disconnected: " + Context.ConnectionId);
            _lobbyService.SetDisconnectingPlayerOffline(Context.UserIdentifier).Wait();
            GetWaitingPlayers().Wait();


            return base.OnDisconnectedAsync(exception);
        }
    }
}

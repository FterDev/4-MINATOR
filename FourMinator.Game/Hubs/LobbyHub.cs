using FourMinator.GameServices.Services;
using FourMinator.Persistence.Domain.Game;
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
        private readonly IMatchService _matchService;

        public LobbyHub(ILobbyService lobbyService, IMatchService matchService) 
        {
            _lobbyService = lobbyService;
            _matchService = matchService;
        }


        public async Task GetWaitingPlayers()
        {
            var players = await _lobbyService.GetWaitingPlayers();
            await Clients.All.SendAsync("ReceiveWaitingPlayers", players);
        }


        public async Task RequestMatch(uint playerId)
        {
            var involvedUsers = await _lobbyService.RequestMatch(playerId, Context.UserIdentifier);
            var requester = involvedUsers["requester"];
            var target = involvedUsers["target"];

            var waitingPlayers = new List<string> { requester.User!.ExternalId, target.User!.ExternalId };

            await GetWaitingPlayers();
            var match = await _matchService.CreateMatch(requester.Id, target.Id);            
            await Clients.User(target.User!.ExternalId).SendAsync("ReceiveMatchRequest", requester.User);
            await Clients.Users(waitingPlayers).SendAsync("ReceivePendingMatch", match);

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

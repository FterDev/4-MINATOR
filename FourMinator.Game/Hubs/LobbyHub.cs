using FourMinator.GameServices.Services;
using FourMinator.Persistence.Domain.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<LobbyHub> _logger;

        public LobbyHub(ILogger<LobbyHub> logger,  ILobbyService lobbyService, IMatchService matchService) 
        {
            _lobbyService = lobbyService;
            _matchService = matchService;
            _logger = logger;
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
            await Clients.User(requester.User!.ExternalId).SendAsync("ReceiveTargetUser", target.User);
            await Clients.Users(waitingPlayers).SendAsync("ReceivePendingMatch", match);
        }


        public async Task RequestMatchBot(short botLevel)
        {
            var match = await _matchService.CreateMatchAgainstBot(Context.UserIdentifier, botLevel);
            await Clients.Caller.SendAsync("ReceiveMatchAccepted", match.Id);
        }

        

        public async Task CancelMatch(Guid matchId)
        {
            var match = await _matchService.GetMatchById(matchId);
            await _matchService.CancelMatch(matchId);
            var  player1 = await _lobbyService.SetPlayerOnline(match.PlayerYellowId);
            var player2 = await _lobbyService.SetPlayerOnline(match.PlayerRedId);
            await Clients.Users(player1, player2).SendAsync("ReceiveMatchCanceled", matchId);
            await GetWaitingPlayers();
        }

        public async Task AcceptMatch(Guid matchId)
        {
            var match = await _matchService.GetMatchById(matchId);
            var player1 = await _lobbyService.SetPlayerOnline(match.PlayerYellowId);
            var player2 = await _lobbyService.SetPlayerOnline(match.PlayerRedId);
            await Clients.Users(player1, player2).SendAsync("ReceiveMatchAccepted", match.Id);
        }

        public override Task OnConnectedAsync()
        {
            
            _logger.LogInformation("Client connected: " + Context.ConnectionId);
            
            _lobbyService.SetConnectingPlayerOnline(Context.UserIdentifier).Wait();
            GetWaitingPlayers().Wait();

            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation("Client disconnected: " + Context.ConnectionId);
            _lobbyService.SetDisconnectingPlayerOffline(Context.UserIdentifier).Wait();
            GetWaitingPlayers().Wait();


            return base.OnDisconnectedAsync(exception);
        }

    }
}

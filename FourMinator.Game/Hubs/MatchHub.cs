using FourMinator.GameServices.Persistence.Contracts;
using FourMinator.GameServices.Persistence.Repository;
using FourMinator.GameServices.Services;
using FourMinator.Persistence;
using FourMinator.Persistence.Domain.Game;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.GameServices.Hubs
{
    public class MatchHub : Hub
    {
        private readonly IMatchService _matchService;
        private readonly IPlayerRepository _playerRepository;
 
        public MatchHub(IMatchService matchService, FourminatorContext context)
        {
            _matchService = matchService;
            _playerRepository = new PlayerRepository(context);
        }

        public async Task JoinMatch(Guid matchId)
        {
            var match = await _matchService.GetMatchById(matchId);

            if(match.State == (short)MatchState.Pending)
                await _matchService.UpdateMatchState(matchId, MatchState.Active);

            await Groups.AddToGroupAsync(Context.ConnectionId, matchId.ToString());
            await Clients.Group(matchId.ToString()).SendAsync("ReceiveMatch", match);
        }


        public async Task GetPlayers(Guid matchId)
        { 
            var match = await _matchService.GetMatchById(matchId);

            var players = new List<Player>();
            players.Add(await _playerRepository.GetPlayerById(match.PlayerYellowId));
            players.Add(await _playerRepository.GetPlayerById(match.PlayerRedId));
            await Clients.Caller.SendAsync("ReceivePlayers", players);
        }


        
    }
}

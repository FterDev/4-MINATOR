
﻿using FourMinator.BotLogic;
using FourMinator.GameServices.Persistence.Contracts;
using FourMinator.GameServices.Persistence.Repository;
using FourMinator.GameServices.Services;
using FourMinator.Persistence;
using FourMinator.Persistence.Domain.Game;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
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

        private readonly Solver _solver;
 
        public MatchHub(IMatchService matchService, FourminatorContext context, Solver solver)
        {
            _matchService = matchService;
            _playerRepository = new PlayerRepository(context);
            _solver = solver;
        }

        public async Task JoinMatch(Guid matchId)
        {
            var match = await _matchService.GetMatchById(matchId);

            if(match.State == (short)MatchState.Pending)
                await _matchService.UpdateMatchState(matchId, MatchState.Active);


            await _matchService.SetMatchStartAndEndTime(matchId);           


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

        public async Task GetGameBoard(string matchId)
        {
            var matchGuid = Guid.Parse(matchId);
            var gameBoard = await _matchService.GetGameBoard(matchGuid);
                        
            await Clients.Group(matchId.ToString()).SendAsync("ReceiveGameBoard", JsonConvert.SerializeObject(gameBoard));


        }

        public async Task MakeMove(int move, string matchId, bool isBot)

        {
            var matchGuid = Guid.Parse(matchId);
            var gameBoard = await _matchService.GetGameBoard(matchGuid);
            gameBoard.MakeMove(move);


            if(isBot)
            {
                await Clients.Group(matchId.ToString()).SendAsync("ReceiveGameBoard", JsonConvert.SerializeObject(gameBoard));
                await Task.Delay(1500);

                var scores = _solver.Analyze(gameBoard.Position, false, 0.0);
                int bestScore = scores.Max();
                var moveMade = false;
                List<int> bestMoves = new List<int>();
                
                for (int i = 0; i < scores.Count; i++)
                {
                    if (scores[i] == bestScore && !moveMade)
                    {
                        bestMoves.Add(i);
                    }
                }

               
                Random random = new Random();
                int randomMove = bestMoves[random.Next(bestMoves.Count)];
                gameBoard.MakeMove(randomMove);

                await Clients.Group(matchId.ToString()).SendAsync("ReceiveGameBoard", JsonConvert.SerializeObject(gameBoard));
            }

            await Clients.Group(matchId.ToString()).SendAsync("ReceiveGameBoard", JsonConvert.SerializeObject(gameBoard));
        }


        
    }
}

using FourMinator.Persistence.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.GameServices.Services
{
    public interface IMatchService
    {

        ICollection<IGameBoard> GameBoards { get; }
        Task<Match> CreateMatch(uint player1, uint player2);
        Task<IGameBoard> GetGameBoard(Guid matchId);
        Task UpdateMatchState(Guid matchId, MatchState state);
        Task SetMatchStartAndEndTime(Guid matchId);
        Task UpdateMatchWinner(Guid matchId, uint winnerId);
        Task<Match> GetMatchById(Guid matchId);
        Task AbortMatch(Guid matchId);
        Task CancelMatch(Guid matchId);
        Task<IEnumerable<Match>> GetMatchesByPlayerId(uint playerId);
        Task<Match> CreateMatchAgainstBot(string externalId, short botLevel);
        Task BotMove(Guid matchId);
        Task FirstBotMove(Guid matchId);

    }
}

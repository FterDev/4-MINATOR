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
        Task<Match> CreateMatch(uint player1, uint player2);
        Task UpdateMatchState(Guid matchId, MatchState state);
        Task UpdateMatchWinner(Guid matchId, uint winnerId);
        Task UpdateMatchStones(Guid matchId, Int16 yellowStones, Int16 redStones);
        Task<Match> GetMatchById(Guid matchId);
        Task<IEnumerable<Match>> GetMatchesByPlayerId(uint playerId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FourMinator.GameServices.Persistence.Contracts
{
    public interface IMatchRepository
    {
        Task CreateMatch(uint playerYellowId, uint playerRedId);
        Task UpdateMatchState(Guid matchId, Int16 state);
        Task UpdateMatchWinner(Guid matchId, uint winnerId);
        Task UpdateMatchStones(Guid matchId, Int16 yellowStones, Int16 redStones);
        Task<Match> GetMatchById(Guid matchId);
        Task<IEnumerable<Match>> GetMatchesByPlayerId(uint playerId);
    }
}

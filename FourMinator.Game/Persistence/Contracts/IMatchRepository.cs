using FourMinator.Persistence.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace FourMinator.GameServices.Persistence.Contracts
{
    public interface IMatchRepository
    {
        Task<Match> CreateMatch(uint playerYellowId, uint playerRedId);
        Task UpdateMatchState(Guid matchId, MatchState state);
        Task UpdateMatchWinner(Guid matchId, uint winnerId);
        Task<Match> GetMatchById(Guid matchId);
        Task SetMatchStartAndEndTime(Guid matchId, DateTime startTime, DateTime endTime);
        Task<IEnumerable<Match>> GetMatchesByPlayerId(uint playerId);
        Task DeleteMatch(Match match);
    }
}

using FourMinator.GameServices.Persistence.Contracts;
using FourMinator.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourMinator.Persistence.Domain.Game;
using Microsoft.EntityFrameworkCore;

namespace FourMinator.GameServices.Persistence.Repository
{
    internal class MatchRepository : IMatchRepository
    {
        private readonly FourminatorContext _context;

        public MatchRepository(FourminatorContext context) {
        
            _context = context;
        
        }
        public async Task<Match> CreateMatch(uint playerYellowId, uint playerRedId)
        {
            var matchId = Guid.NewGuid();
            var match = new Match
            {
                Id = matchId,
                PlayerYellowId = playerYellowId,
                PlayerRedId = playerRedId,
                State = 0,
          
            };

            await _context.Matches.AddAsync(match);
            await _context.SaveChangesAsync();
            return match;
        }

        public async Task DeleteMatch(Match match)
        {
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
        }

        public async Task<Match> GetMatchById(Guid matchId)
        {
            return await _context.Matches.Where(m => m.Id == matchId).Include(p => p.PlayerYellow).Include(p => p.PlayerRed).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Match>> GetMatchesByPlayerId(uint playerId)
        {
            return await _context.Matches.Where(m => m.PlayerYellowId == playerId || m.PlayerRedId == playerId).ToListAsync();
        }

        public async Task UpdateMatchState(Guid matchId, MatchState state)
        {
            var match = await _context.Matches.FindAsync(matchId);
            match.State = (Int16)state;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMatchStones(Guid matchId, short yellowStones, short redStones)
        {
            var match = await _context.Matches.FindAsync(matchId);
            match.YellowStones = yellowStones;
            match.RedStones = redStones;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMatchWinner(Guid matchId, uint winnerId)
        {
            var match = await _context.Matches.FindAsync(matchId);
            match.WinnerId = winnerId;
            await _context.SaveChangesAsync();
        }
    }
}

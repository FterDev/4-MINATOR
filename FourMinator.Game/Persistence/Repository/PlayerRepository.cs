using FourMinator.GameServices.Persistence.Contracts;
using FourMinator.Persistence;
using FourMinator.Persistence.Domain.Game;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.GameServices.Persistence.Repository
{
    public class PlayerRepository : IPlayerRepository
    {

        private readonly FourminatorContext _context;

        public PlayerRepository(FourminatorContext context)
        {
            _context = context;
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<Player?> GetPlayerById(uint playerId)
        {
            return await _context.Players.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == playerId);
        }

        public async Task<Player?> GetPlayerByUserId(int userId)
        {
            return await _context.Players.Include(p => p.User).FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<Player?> GetPlayerByExternalId(string externalId)
        {
            return await _context.Players.Include(p => p.User).FirstOrDefaultAsync(p => p.User.ExternalId == externalId);
        }


        public async Task UpdatePlayerState(uint playerId, PlayerState state)
        {
            var player = await _context.Players.FirstAsync(p => p.Id == playerId && p.IsBot == false);
            player.State = (Int16)state;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Player>> GetAllOnlinePlayers()
        {
            return await _context.Players.Where(p => p.State == (Int16)PlayerState.Online && p.IsBot == false).Include(p => p.User).ToListAsync();
        }

        public async Task<Player> GetBot()
        {
            return await _context.Players.FirstAsync(p => p.IsBot == true);
        }
    }
}

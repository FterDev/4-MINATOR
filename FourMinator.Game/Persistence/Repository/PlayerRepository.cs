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
            return await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);
        }

        public async Task<Player?> GetPlayerByUserId(int userId)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task UpdatePlayerState(uint playerId, PlayerState state)
        {
            var player = await _context.Players.FirstAsync(p => p.Id == playerId);
            player.State = (Int16)state;
            await _context.SaveChangesAsync();
        }
    }
}

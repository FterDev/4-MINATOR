using FourMinator.GameServices.Persistence.Contracts;
using FourMinator.GameServices.Persistence.Repository;
using FourMinator.Persistence;
using FourMinator.Persistence.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.GameServices.Services
{
    public class LobbyService : ILobbyService
    {

        private readonly IPlayerRepository _playerRepository;

        public LobbyService(FourminatorContext context)
        {
            _playerRepository = new PlayerRepository(context);
        }

        public async Task<IEnumerable<Player>> GetWaitingPlayers()
        {
            return await _playerRepository.GetAllOnlinePlayers();
        }

        public async Task SetDisconnectingPlayerOffline(string externalId)
        {
            var player = await _playerRepository.GetPlayerByExternalId(externalId);
            if (player != null)
            {
                await _playerRepository.UpdatePlayerState(player.Id, PlayerState.Offline);
            }
        }

        public async Task SetConnectingPlayerOnline(string externalId)
        {
            var player = await _playerRepository.GetPlayerByExternalId(externalId);
            if (player != null)
            {
                await _playerRepository.UpdatePlayerState(player.Id, PlayerState.Online);
            }
        }
    }
}

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

        public async Task<string> SetPlayerOnline(uint id)
        {
            var player = await _playerRepository.GetPlayerById(id);
            if (player != null)
            {
                await _playerRepository.UpdatePlayerState(player.Id, PlayerState.Online);
                return player.User!.ExternalId;
            }
            return "";
        }

        public async Task<IDictionary<string, Player>> RequestMatch(uint playerId, string requester)
        {
            var targetPlayer = await _playerRepository.GetPlayerById(playerId);
            var requestingPlayer = await _playerRepository.GetPlayerByExternalId(requester);
            await _playerRepository.UpdatePlayerState(playerId, PlayerState.MatchMaking);
            await _playerRepository.UpdatePlayerState(requestingPlayer.Id, PlayerState.MatchMaking);
            
            return new Dictionary<string, Player>
            {
                { "target", targetPlayer },
                { "requester", requestingPlayer }
            };
            
        }


        
    }
}

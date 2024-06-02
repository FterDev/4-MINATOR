using FourMinator.Persistence.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.GameServices.Services
{
    public interface ILobbyService
    {
        Task SetConnectingPlayerOnline(string externalId);
        Task SetDisconnectingPlayerOffline(string externalId);
        Task<IEnumerable<Player>> GetWaitingPlayers();
        Task<IDictionary<string, Player>> RequestMatch(uint playerId, string requester);

    }
}

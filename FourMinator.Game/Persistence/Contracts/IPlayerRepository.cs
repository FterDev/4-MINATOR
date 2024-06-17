using FourMinator.Persistence.Domain.Game;


namespace FourMinator.GameServices.Persistence.Contracts
{
    public interface IPlayerRepository
    {

        public Task<Player> CreatePlayer(Player player);

        public Task UpdatePlayerState(uint playerId, PlayerState state);

        public Task<Player?> GetPlayerById(uint playerId);

        public Task<Player?> GetPlayerByUserId(int userId);

        public Task<Player?> GetPlayerByExternalId(string externalId);

        public Task<IEnumerable<Player>> GetAllOnlinePlayers();
        public Task<Player> GetBot();

    }
}

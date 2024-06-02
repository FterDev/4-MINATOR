using FourMinator.GameServices.Persistence.Contracts;
using FourMinator.GameServices.Persistence.Repository;
using FourMinator.Persistence;
using FourMinator.Persistence.Domain.Game;

namespace FourMinator.GameServices.Services
{
    public class MatchService : IMatchService
    {

        private readonly IMatchRepository _matchRepository;
        public MatchService(FourminatorContext context) { 
        
            _matchRepository = new MatchRepository(context);

        }

        public async Task AbortMatch(Guid matchId)
        {
            await this.UpdateMatchState(matchId, MatchState.Aborted);
        }

        public async Task<Match> CreateMatch(uint player1, uint player2)
        {
            var playerYellowId = RandomColorAssignmnent() ? player1 : player2;
            var playerRedId = playerYellowId == player1 ? player2 : player1;
            return await _matchRepository.CreateMatch(playerYellowId, playerRedId);
        }

        public async Task<Match> GetMatchById(Guid matchId)
        {
            return await _matchRepository.GetMatchById(matchId);
        }

        public async Task<IEnumerable<Match>> GetMatchesByPlayerId(uint playerId)
        {
            return await _matchRepository.GetMatchesByPlayerId(playerId);
        }

        public async Task UpdateMatchState(Guid matchId, MatchState state)
        {
            await _matchRepository.UpdateMatchState(matchId, state);
        }

        public async Task UpdateMatchStones(Guid matchId, short yellowStones, short redStones)
        {
            await _matchRepository.UpdateMatchStones(matchId, yellowStones, redStones);
        }

        public async Task UpdateMatchWinner(Guid matchId, uint winnerId)
        {
            await _matchRepository.UpdateMatchWinner(matchId, winnerId);
        }

        private bool RandomColorAssignmnent()
        {
            return new Random().Next(0, 2) == 0;
        }
    }
}

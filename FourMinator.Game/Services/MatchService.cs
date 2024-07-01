using FourMinator.BotLogic;
using FourMinator.GameServices.Persistence.Contracts;
using FourMinator.GameServices.Persistence.Repository;
using FourMinator.Persistence;
using FourMinator.Persistence.Domain.Game;

namespace FourMinator.GameServices.Services
{
    public class MatchService : IMatchService
    {

        private ICollection<IGameBoard> _gameBoards;

        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly Solver _solver;
        public MatchService(FourminatorContext context, ICollection<IGameBoard> gameBoards, Solver solver) { 
        
            _matchRepository = new MatchRepository(context);
            _playerRepository = new PlayerRepository(context);
            _solver = solver;
            _gameBoards = gameBoards;

        }

        public ICollection<IGameBoard> GameBoards => _gameBoards;
        public async Task AbortMatch(Guid matchId)
        {
            await this.UpdateMatchState(matchId, MatchState.Aborted);
        }

        public async Task CancelMatch(Guid matchId)
        {
            var match = await _matchRepository.GetMatchById(matchId);
            await _matchRepository.DeleteMatch(match);

        }

        public async Task<Match> CreateMatch(uint player1, uint player2)
        {
            var playerYellowId = RandomColorAssignmnent() ? player1 : player2;
            var playerRedId = playerYellowId == player1 ? player2 : player1;
            
            return await _matchRepository.CreateMatch(playerYellowId, playerRedId);
        }

        public async Task<Match> CreateMatchAgainstBot(string externalId, ushort botLevel)
        {
            var player = await _playerRepository.GetPlayerByExternalId(externalId);
            var bot = await _playerRepository.GetBot();
            var playerYellowId = RandomColorAssignmnent() ? player : bot;
            var playerRedId = playerYellowId == player ? bot : player;
            var match = await _matchRepository.CreateMatch(playerYellowId.Id, playerRedId.Id, botLevel);
            return match;
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



        public async Task UpdateMatchWinner(Guid matchId, uint winnerId)
        {
            await _matchRepository.UpdateMatchWinner(matchId, winnerId);
        }

        public Task<IGameBoard> GetGameBoard(Guid matchId)
        {
            var gameBoard = _gameBoards.FirstOrDefault(x => x.Id == matchId);
            if (gameBoard == null)
            {
                gameBoard = new GameBoard(matchId);
               
                _gameBoards.Add(gameBoard);
            }
            return Task.FromResult(gameBoard);
        }

        private bool RandomColorAssignmnent()
        {
            return new Random().Next(0, 2) == 0;
        }


        public async Task SetMatchStartAndEndTime(Guid matchId)
        {
            var startTime = DateTime.Now;
            var endTime = startTime.AddMinutes(10);
            await _matchRepository.SetMatchStartAndEndTime(matchId, startTime, endTime);
        }

        public async Task BotMove(Guid matchId, ushort botlevel)
        {

            double botStrength = 0.0;
            switch (botlevel)
            {
                case 0:
                    botStrength = 0.3;
                    break;
                case 1:
                    botStrength = 0.1;
                    break;
                case 2:
                    botStrength = 0.0;
                    break;
            }

            await Task.Delay(1500);
            var gameBoard = await GetGameBoard(matchId);
            var scores = _solver.Analyze(gameBoard.Position, false, botStrength);
            int bestScore = scores.Max();
            var moveMade = false;
            List<int> bestMoves = new List<int>();

            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i] == bestScore && !moveMade)
                {
                    bestMoves.Add(i);
                }
            }

            Random random = new Random();
            int randomMove = bestMoves[random.Next(bestMoves.Count)];
            gameBoard.MakeMove(randomMove);
        }

        public async Task FirstBotMove(Guid matchId)
        {
            await Task.Delay(1500);
            var gameBoard = await GetGameBoard(matchId);
            gameBoard.MakeMove(3);
            
        }   

    }
}

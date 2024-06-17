namespace FourMinator.BotLogic
{
    public class Solver
    {
        private ulong nodeCount;
        private int[] columnOrder = new int[Position.WIDTH];
        private OpeningBook _book;

        private TranspositionTable<ulong, int> transTable;
        private const int TABLE_SIZE = 24;
        private Random random;

        public Solver(OpeningBook book)
        {
            nodeCount = 0;
            transTable = new TranspositionTable<ulong, int>(TABLE_SIZE);
            // -> book = new OpeningBook(Position.WIDTH, Position.HEIGHT);
            _book = book;

            columnOrder = new int[] { 3, 2, 4, 1, 5, 0, 6 };
            random = new Random();
        }

        private int Negamax(Position position, int alpha, int beta)
        {
            nodeCount++;

            ulong possibleMoves = position.PossibleNonLoosingMoves();
            if (possibleMoves == 0)
            {
                return -(Position.WIDTH * Position.HEIGHT - (int)position.GetMoveCount()) / 2;
            }

            if (position.GetMoveCount() >= Position.WIDTH * Position.HEIGHT - 2) // Draw
                return 0;

            int minScore = -(Position.WIDTH * Position.HEIGHT - 2 - (int)position.GetMoveCount()) / 2;
            if (alpha < minScore)
            {
                alpha = minScore;
                if (alpha >= beta) return alpha;
            }

            int maxScore = (Position.WIDTH * Position.HEIGHT - 1 - (int)position.GetMoveCount()) / 2;
            if (beta > maxScore)
            {
                beta = maxScore;
                if (alpha >= beta)
                {
                    return beta;
                }
            }

            ulong key = position.Key();
            int value;

            if ((value = transTable.Get(key)) != 0)
            {
                if (value > Position.MAX_SCORE - Position.MIN_SCORE + 1) // lower bound
                {
                    minScore = value + 2 * Position.MIN_SCORE - Position.MAX_SCORE - 2;
                    if (alpha < minScore)
                    {
                        alpha = minScore;
                        if (alpha >= beta) return alpha;
                    }
                }
                else // upper bound
                {
                    maxScore = value + Position.MIN_SCORE - 1;
                    if (beta > maxScore)
                    {
                        beta = maxScore;
                        if (alpha >= beta) return beta;
                    }
                }
            }



            if ((value = _book.Get(position)) != 0)

            {
                return value + Position.MIN_SCORE - 1;
            }

            MoveSorter moveSorter = new MoveSorter();

            for (int i = Position.WIDTH; i-- > 0;)
            {
                ulong move = possibleMoves & Position.ColumnMaskCol(columnOrder[i]);
                if (move != 0)
                {
                    moveSorter.Add(move, position.MoveScore(move));
                }
            }

            ulong nextMove;
            while ((nextMove = moveSorter.GetNext()) != 0)
            {
                Position newPosition = position.Clone();
                newPosition.Play(nextMove);
                int score = -Negamax(newPosition, -beta, -alpha);

                if (score >= beta)
                {
                    transTable.Put(key, score + Position.MAX_SCORE - 2 * Position.MIN_SCORE + 2);
                    return score;
                }

                if (score > alpha)
                {
                    alpha = score;
                }
            }

            transTable.Put(key, alpha - Position.MIN_SCORE + 1);
            return alpha;
        }

        public int Solve(Position position, bool weak = false)
        {
            nodeCount = 0;

            int minScore = -(Position.WIDTH * Position.HEIGHT - (int)position.GetMoveCount()) / 2;
            int maxScore = (Position.WIDTH * Position.HEIGHT + 1 - (int)position.GetMoveCount()) / 2;

            if (position.CanWinNext())
            {
                return (Position.WIDTH * Position.HEIGHT + 1 - (int)position.GetMoveCount()) / 2;
            }

            if (weak)
            {
                minScore = -1;
                maxScore = 1;
            }

            while (minScore < maxScore)
            {
                int midScore = minScore + (maxScore - minScore) / 2;
                if (midScore <= 0 && minScore / 2 < midScore)
                {
                    midScore = minScore / 2;
                }
                else if (midScore >= 0 && maxScore / 2 > midScore)
                {
                    midScore = maxScore / 2;
                }

                int result = Negamax(position, midScore, midScore + 1);

                if (result <= midScore)
                {
                    maxScore = result;
                }
                else
                {
                    minScore = result;
                }

            }

            return minScore;
        }

        public List<int> Analyze(Position position, bool weak = false, double randomness = 0.0)
        {
            List<int> scores = new List<int>(new int[Position.WIDTH]);
            for (int column = 0; column < Position.WIDTH; column++)
            {
                if (position.CanPlay(column))
                {
                    if (position.IsWinningMove(column))
                    {
                        scores[column] = (Position.WIDTH * Position.HEIGHT + 1 - (int)position.GetMoveCount()) / 2;
                    }
                    else
                    {
                        Position newPosition = position.Clone();
                        newPosition.PlayCol(column);
                        scores[column] = -Solve(newPosition, weak);
                    }
                }
                else
                {
                    scores[column] = -1000; // INVALID_MOVE
                }
            }
            if (randomness > 0.0)
            {
                for (int i = 0; i < scores.Count; i++)
                {
                    if (random.NextDouble() < randomness)
                    {
                        scores[i] += random.Next(-3, 4);
                    }
                }
            }

            return scores;
        }

        public ulong GetNodeCount()
        {
            return nodeCount;
        }


        public void ResetSolver()
        {
            nodeCount = 0;
            transTable.Reset();
        }

    }
}

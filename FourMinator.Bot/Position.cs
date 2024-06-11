namespace FourMinator.BotLogic
{
    public class Position
    {
        public const int WIDTH = 7;
        public const int HEIGHT = 6;

        public const int MIN_SCORE = -(WIDTH * HEIGHT) / 2 + 3;
        public const int MAX_SCORE = (WIDTH * HEIGHT + 1) / 2 - 3;

        private ulong currentPosition;
        private ulong mask;
        private uint moves;

        private static readonly ulong BottomMask = Bottom(WIDTH, HEIGHT);
        private static readonly ulong BoardMask = BottomMask * ((1UL << HEIGHT) - 1);

        public Position()
        {
            currentPosition = 0;
            mask = 0;
            moves = 0;
        }

        public Position Clone()
        {
            return (Position)this.MemberwiseClone(); // Shallow copy
        }

        public void Play(ulong move)
        {
            currentPosition ^= mask;
            mask |= move;
            moves++;
        }

        public uint Play(string sequence)
        {
            for (uint i = 0; i < sequence.Length; i++)
            {
                int column = sequence[(int)i] - '1';
                if (column < 0 || column >= WIDTH || !CanPlay(column) || IsWinningMove(column))
                {
                    return i;
                }
                PlayCol(column);
            }
            return (uint)sequence.Length;
        }

        public bool CanWinNext()
        {
            return (WinningPosition() & Possible()) != 0;
        }

        public uint GetMoveCount()
        {
            return moves;
        }

        public ulong Key()
        {
            return currentPosition + mask;
        }

        public ulong Key3()
        {
            ulong keyForward = 0;
            for (int i = 0; i < WIDTH; i++) PartialKey3(ref keyForward, i);

            ulong keyReverse = 0;
            for (int i = WIDTH; i-- > 0;) PartialKey3(ref keyReverse, i);

            return keyForward < keyReverse ? keyForward / 3 : keyReverse / 3;
        }

        public ulong PossibleNonLoosingMoves()
        {
            if (!CanWinNext())
            {
                ulong possibleMask = Possible();
                ulong opponentWin = OpponentWinningPosition();
                ulong forcedMoves = possibleMask & opponentWin;
                if (forcedMoves != 0)
                {
                    if ((forcedMoves & (forcedMoves - 1)) != 0)
                    {
                        return 0; // Opponent has two winning moves, and you can't stop them
                    }
                    else
                    {
                        possibleMask = forcedMoves; // Force playing the single forced move
                    }
                }
                return possibleMask & ~(opponentWin >> 1); // Avoid playing below an opponent's winning spot
            }
            else
            {
                return 0;
            }
        }

        public int MoveScore(ulong move)
        {
            return PopCount(ComputeWinningPosition(currentPosition | move, mask));
        }

        public bool CanPlay(int column)
        {
            return (mask & TopMaskCol(column)) == 0;
        }

        public void PlayCol(int column)
        {
            Play((mask + BottomMaskCol(column)) & ColumnMaskCol(column));
        }

        public bool IsWinningMove(int column)
        {
            return (WinningPosition() & Possible() & ColumnMaskCol(column)) != 0;
        }

        private ulong WinningPosition()
        {
            return ComputeWinningPosition(currentPosition, mask);
        }

        private ulong OpponentWinningPosition()
        {
            return ComputeWinningPosition(currentPosition ^ mask, mask);
        }

        private ulong Possible()
        {
            return (mask + BottomMask) & BoardMask;
        }
        private static int PopCount(ulong bitmask)
        {
            int count;
            for (count = 0; bitmask != 0; count++)
            {
                bitmask &= bitmask - 1;
            }
            return count;
        }

        private static ulong ComputeWinningPosition(ulong position, ulong mask)
        {
            // Vertical
            ulong result = (position << 1) & (position << 2) & (position << 3);

            // Horizontal
            ulong potential = (position << (HEIGHT + 1)) & (position << 2 * (HEIGHT + 1));
            result |= potential & (position << 3 * (HEIGHT + 1));
            result |= potential & (position >> (HEIGHT + 1));
            potential = (position >> (HEIGHT + 1)) & (position >> 2 * (HEIGHT + 1));
            result |= potential & (position << (HEIGHT + 1));
            result |= potential & (position >> 3 * (HEIGHT + 1));

            // Diagonal 1 (\)
            potential = (position << HEIGHT) & (position << 2 * HEIGHT);
            result |= potential & (position << 3 * HEIGHT);
            result |= potential & (position >> HEIGHT);
            potential = (position >> HEIGHT) & (position >> 2 * HEIGHT);
            result |= potential & (position << HEIGHT);
            result |= potential & (position >> 3 * HEIGHT);

            // Diagonal 2 (/)
            potential = (position << (HEIGHT + 2)) & (position << 2 * (HEIGHT + 2));
            result |= potential & (position << 3 * (HEIGHT + 2));
            result |= potential & (position >> (HEIGHT + 2));
            potential = (position >> (HEIGHT + 2)) & (position >> 2 * (HEIGHT + 2));
            result |= potential & (position << (HEIGHT + 2));
            result |= potential & (position >> 3 * (HEIGHT + 2));

            return result & (BoardMask ^ mask);
        }

        public void PartialKey3(ref ulong key, int column)
        {
            ulong pos = 1UL << (column * (HEIGHT + 1));
            while ((pos & mask) != 0)
            {
                key *= 3;
                if ((pos & currentPosition) != 0)
                {
                    key += 1;
                }
                else
                {
                    key += 2;
                }
                pos <<= 1;
            }
            key *= 3;
        }

        private static ulong Bottom(int width, int height)
        {
            return width == 0 ? 0 : Bottom(width - 1, height) | (1UL << (width - 1) * (height + 1));
        }

        private static ulong TopMaskCol(int column)
        {
            return (1UL << (HEIGHT - 1)) << column * (HEIGHT + 1);
        }

        private static ulong BottomMaskCol(int column)
        {
            return 1UL << column * (HEIGHT + 1);
        }

        public static ulong ColumnMaskCol(int column)
        {
            return ((1UL << HEIGHT) - 1) << column * (HEIGHT + 1);
        }
    }
}
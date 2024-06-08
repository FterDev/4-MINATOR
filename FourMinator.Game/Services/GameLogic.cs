using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.GameServices.Services
{
    internal class GameLogic
    {
        public static bool CheckWin(short[,] board, short player)
        {
            return CheckHorizontalWin(board, player) ||
                   CheckVerticalWin(board, player) ||
                   CheckDiagonalWin(board, player);
        }

        private static bool CheckHorizontalWin(short[,] board, short player)
        {
            for (int row = 0; row < 6; row++)
            {
                for (int column = 0; column < 7 - 3; column++)
                {
                    if (board[column, row] == player &&
                        board[column + 1, row] == player &&
                        board[column + 2, row ] == player &&
                        board[column + 3, row] == player)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool CheckVerticalWin(short[,] board, short player)
        {
            for (int row = 0; row < 6 - 3; row++)
            {
                for (int column = 0; column < 7; column++)
                {
                    if (board[column, row] == player &&
                        board[column, row + 1] == player &&
                        board[column, row + 2] == player &&
                        board[column, row + 3] == player)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool CheckDiagonalWin(short[,] board, short player)
        {
            // Check for diagonals left \
            for (int row = 0; row < 6 - 3; row++)
            {
                for (int column = 0; column < 7 - 3; column++)
                {
                    if (board[column, row] == player &&
                        board[column + 1, row + 1] == player &&
                        board[column + 2, row + 2] == player &&
                        board[column + 3, row + 3] == player)
                    {
                        return true;
                    }
                }
            }

            // Check for diagonals right /
            for (int row = 3; row < 6; row++)
            {
                for (int column = 0; column < 7 - 3; column++)
                {
                    if (board[column, row] == player &&
                        board[column + 1, row - 1] == player &&
                        board[column + 2, row - 2] == player &&
                        board[column + 3, row - 3] == player)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

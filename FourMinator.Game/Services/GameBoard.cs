using FourMinator.BotLogic;
using FourMinator.Persistence.Domain.Game;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.GameServices.Services
{
    internal class GameBoard : IGameBoard
    {


        private readonly short _rows = 6;
        private readonly short _columns = 7;
        private readonly Position _position = new Position();
        private readonly short[,] _board;
        private readonly Guid _id;
        private string _moveSequence;
        private short _currentPlayer;
        private short _winner;
        private short _moveCount;




        public short CurrentPlayer => _currentPlayer;

        public short[,] Board => _board;

        public short Winner => _winner;

        public short Moves => _moveCount;

        public Position Position => _position;

        public string MoveSequence => _moveSequence;

        public Guid Id => _id;

        public GameBoard(Guid matchId)
        {
            _winner = 0;
            _moveCount = 0;
            _id = matchId;
            _moveSequence = "";

            _board = new short[_columns, _rows];

            SetRandomPlayer();
        }


        public void MakeMove(int x)
        {
            for (int row = _rows - 1; row >= 0; row--)
            {
                if (_board[x, row] == 0)
                {
                    _board[x, row] = _currentPlayer;
                    _position.PlayCol(x);

                    if(GameLogic.CheckWin(_board, _currentPlayer))
                    {
                        _winner = _currentPlayer;
                    }

                    _moveSequence += x.ToString();
                    _currentPlayer *= -1;
                    break;
                }
            }

            
        }


        private void SetRandomPlayer()
        {
            Random rnd = new Random();
            var randNumber = (short)rnd.Next(1,3);

            _currentPlayer = randNumber % 2 == 0 ? (short)-1 : (short)1;
        }

    }
}

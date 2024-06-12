using FourMinator.BotLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.GameServices.Services
{
    public interface IGameBoard
    {
        public Guid Id { get; }
        public short CurrentPlayer { get; }
        public short[,] Board { get; }
        public short Moves { get; }
        public short Winner { get; }

        public Position Position { get; }

        public string MoveSequence { get; }
        public void MakeMove(int x);

    }
}

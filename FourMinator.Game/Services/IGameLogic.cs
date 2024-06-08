using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.GameServices.Services
{
    public interface IGameLogic
    {
        public bool CheckWin(short[,] board, short player);
    }
}

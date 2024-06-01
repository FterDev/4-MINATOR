using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.Persistence.Domain.Game
{
    public class Player
    {
        public uint Id { get; set; }
        public int? UserId { get; set; }
        public bool IsBot { get; set; }
        public Int16 State { get; set; }
        public User? User { get; set; }

    }


    public enum PlayerState
    {
        Offline = -1,
        Online = 0,
        MatchMaking = 1,
        Playing = 2
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.Persistence.Domain.Game
{
    public class MatchMoves
    {
        public Guid MatchId { get; set; }
        public Int16 MoveNumber { get; set; }
        public Int16 X { get; set; }
        public Int16 Y { get; set; }
        public uint PlayerId { get; set; }
        public bool Color { get; set; }
        public bool Skipped { get; set; }
        public bool Joker { get; set; }
        public uint MoveTime { get; set; }
        public DateTime MoveTimestamp { get; set; }
        public Match Match { get; set; }
        public Player Player { get; set; }
    }
}

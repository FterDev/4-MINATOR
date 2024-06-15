using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.Persistence.Domain.Game
{
    public class Match
    {
        public Guid Id { get; set; }
        public uint PlayerYellowId { get; set; }
        public uint PlayerRedId { get; set; }
        public uint? RobotId { get; set; }
        public uint? WinnerId { get; set; }
        public Int16 State { get; set; } = (Int16)MatchState.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public DateTime? AbortedAt { get; set; }

        public Player? PlayerYellow { get; set; }
        public Player? PlayerRed { get; set; }
        public Player? PlayerWinner { get; set; }
        public Robot? Robot { get; set; }
        public ICollection<MatchMoves> Moves { get; set; }
    }

    public enum MatchState
    {
        Aborted = -1,
        Pending = 0,
        Active = 1,
        Finished = 2
    }
}

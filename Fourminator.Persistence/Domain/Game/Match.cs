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
        public int? Robot { get; set; }
        public uint? Winner { get; set; }
        public Int16 YellowStones { get; set; }
        public Int16 RedStones { get; set; }
        public Int16 State { get; set; }
        public Player PlayerYellow { get; set; }
        public Player PlayerRed { get; set; }
        public Robot RobotForMatch { get; set; }
    }

    public enum MatchState
    {
        Aborted = -1,
        Pending = 0,
        Active = 1,
        Finished = 2
    }
}

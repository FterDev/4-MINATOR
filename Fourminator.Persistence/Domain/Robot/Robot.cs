


using FourMinator.Persistence.Domain.Game;
using System.ComponentModel.DataAnnotations.Schema;

namespace FourMinator.Persistence.Domain
{
    public class Robot
    {
        public uint Id { get; set; }
        [Column("username")]
        public string Name { get; set; }
        public int CreatedBy { get; set; }

        [Column("password_hash")]
        public string? Password { get; set; }
        [Column("salt")]
        public string? Salt { get; set; }
        public string? PublicKey { get; set; }
        public Int16 Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public User CreatedByUser { get; set; }
        public ICollection<Match> Matches { get; set; }
    
    }

    public enum RobotStatus
    {
        Offline = -1,
        Online = 0,
        Busy = 1,
        Error = 2
    }
}



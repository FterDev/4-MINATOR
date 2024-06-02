


using FourMinator.Persistence.Domain.Game;

namespace FourMinator.Persistence.Domain
{
    public class Robot
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public string? Password { get; set; }
        public string? Thumbprint { get; set; }
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



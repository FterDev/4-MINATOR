using FourMinator.Auth.Persistence.Domain;


namespace FourMinator.Persistence.Domain
{
    internal class Robot
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public User CreatedBy { get; set; }
        public string? Password { get; set; }
        public string? Thumbprint { get; set; }
        public string? PublicKey { get; set; }
        public Int16 Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}

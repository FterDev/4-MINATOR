using FourMinator.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.Robot
{
    internal class Robot
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public User CreatedBy { get; set; }
        public string Password { get; set; }
        public string Thumbprint { get; set; }
        public string PublicKey { get; set; }
        public Int16 Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

using FourMinator.Persistence.Domain;
using FourMinator.Persistence.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FourMinator.Persistence.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string ExternalId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Robot> Robots { get; set; }
        [JsonIgnore]
        public Player Player { get; set; }
    }
}

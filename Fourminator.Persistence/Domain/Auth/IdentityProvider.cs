

namespace FourMinator.Persistence.Domain
{
    public class IdentityProvider
    {
        public Guid IdentityProviderId {get; set;}
        public string Name {get; set;}
        public string AuthKey { get; set; }
        public string Domain { get; set; }
        public string SourceIp { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public object ToQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
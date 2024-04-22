

namespace Fourminator.Auth
{
    public class IdentityProvider
    {
        public Guid IdentityProviderId {get; set;}
        public int Id {get; set;}
        public string Name {get; set;}
        public string AuthKey { get; set; }
        public string ClientSecret { get; set; }
        public string ClientId { get; set; }
        public string Domain { get; set; }
        public string SourceIp { get; set; }
        public bool IsActive { get; set; }
    }
}
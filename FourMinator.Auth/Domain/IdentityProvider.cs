

namespace FourMinator.Auth
{
    public class IdentityProvider
    {
        public Guid IdentityProviderId {get; set;}
        public string Name {get; set;}
        public string AuthKey { get; set; }
        public string Domain { get; set; }
        public string SourceIp { get; set; }
        public bool IsActive { get; set; }
    }
}
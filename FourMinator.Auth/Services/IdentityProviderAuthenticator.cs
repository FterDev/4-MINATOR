using System.Text;
using FourMinator.Auth;
using FourMinator.Auth.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using RandomString4Net;


namespace Fourminator.Auth
{
    internal class IdentityProviderAuthenticator : IIdentityProviderAuthenticator
    {

        private IIdentityProviderRepository _identityProviderRepository;

        public IdentityProvider IdentityProvider { get; set; }


        public IdentityProviderAuthenticator(IIdentityProviderRepository identityProviderRepository)
        {
            IdentityProvider = new IdentityProvider();
            _identityProviderRepository = identityProviderRepository;
        }

       

        public string DecodeAuthKey(string authKeyBase64)
        {
            byte[] authKeyBytes = Convert.FromBase64String(authKeyBase64);
            string authKey = Encoding.UTF8.GetString(authKeyBytes);
            this.IdentityProvider.AuthKey = authKey;
            return authKey;
        }

        public string GenerateAuthKey()
        {
            var authKey = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE , 64);
            this.IdentityProvider.AuthKey = authKey;
            return authKey;
        }

        public void CreateIdentityProvider(string? identityProviderName = null, string? domain = null, string? sourceIp = null)
        {
            IdentityProvider.IdentityProviderId = Guid.NewGuid();
            IdentityProvider.Name = identityProviderName == null ? "untitled" : identityProviderName;
            IdentityProvider.Domain = domain == null ? "" : domain;
            IdentityProvider.SourceIp = sourceIp == null ? "0.0.0.0" : sourceIp;
            IdentityProvider.IsActive = true;
        }

        

        public bool ValidateAuthKey()
        {
            throw new NotImplementedException();
        }

        public void SaveIdentityProvider()
        {
            throw new NotImplementedException();
        }
    }
}
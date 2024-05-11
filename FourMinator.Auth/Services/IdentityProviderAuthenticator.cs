using System.Text;
using FourMinator.Persistence.Domain;
using Microsoft.EntityFrameworkCore;
using RandomString4Net;


namespace FourMinator.Auth
{
    public class IdentityProviderAuthenticator : IIdentityProviderAuthenticator
    {

        private IIdentityProviderRepository _identityProviderRepository;

        public IdentityProvider IdentityProvider { get; set; }
        

        public IdentityProviderAuthenticator(IIdentityProviderRepository identityProviderRepository)
        {   
            _identityProviderRepository = identityProviderRepository;
        }

       

        public string DecodeAuthKey(string authKeyBase64)
        {
            byte[] authKeyBytes = Convert.FromBase64String(authKeyBase64);
            string authKey = Encoding.UTF8.GetString(authKeyBytes);
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

        public void SaveIdentityProvider()
        {
            var newIdentityProvier = _identityProviderRepository.CreateIdentityProvider(IdentityProvider).Result;
            if (newIdentityProvier.IdentityProviderId != IdentityProvider.IdentityProviderId)
            {
                throw new Exception("Failed to save identity provider - ID mismatch error");
            }
        }


        public bool ValidateAuthKey(string authKeyBase64)
        {
            if (!CheckBase64(authKeyBase64))
            {
                return false;
            }
            var authKey = DecodeAuthKey(authKeyBase64);
            var identityProvider = _identityProviderRepository.GetIdentityProviderByKey(authKey).Result;

            if (identityProvider == null)
            {
                return false;
            }
            IdentityProvider = identityProvider;
            return true;
        }

        private bool CheckBase64(string authKeyBase64)
        {
            try
            {
                Convert.FromBase64String(authKeyBase64);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
    }
}


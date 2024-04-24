
namespace Fourminator.Auth
{
    public interface IIdentityProviderAuthenticator
    {
        IdentityProvider IdentityProvider { get; set; }

        string GenerateAuthKey();
        string DecodeAuthKey(string authKeyBase64);

        void CreateIdentityProvider(string? identityProviderName, string? domain, string? sourceIp)
        void SaveAuthKey(string identityProviderName, string domain, string sourceIp);
        bool ValidateAuthKey();
    }
}
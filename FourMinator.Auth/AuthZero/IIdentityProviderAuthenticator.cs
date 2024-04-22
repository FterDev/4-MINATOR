
namespace Fourminator.Auth
{
    public interface IIdentityProviderAuthenticator
    {
        string AuthKey { get; set; }

        string GenerateAuthKey();
        string DecodeAuthKey(string authKeyBase64);
        void SaveAuthKey();
        bool ValidateAuthKey();
    }
}
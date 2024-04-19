
namespace Fourminator.Auth
{
    public interface IAuthZeroAuthenticator
    {
        string AuthKey { get; set; }

        string GenerateAuthKey();
        string DecodeAuthKey(string authKeyBase64);
        void SaveAuthKey();
        bool ValidateAuthKey();
    }
}
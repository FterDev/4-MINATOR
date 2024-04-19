
namespace Fourminator.Auth
{
    internal class AuthZeroAuthenticator : IAuthZeroAuthenticator
    {

        public string AuthKey { get; set; }

        public AuthZeroAuthenticator()
        {
         
        }
        

        public string DecodeAuthKey(string authKeyBase64)
        {
            throw new NotImplementedException();
        }

        public string GenerateAuthKey()
        {
            throw new System.NotImplementedException();
        }

        public void SaveAuthKey()
        {
            throw new NotImplementedException();
        }

        public bool ValidateAuthKey()
        {
            throw new NotImplementedException();
        }
    }
}
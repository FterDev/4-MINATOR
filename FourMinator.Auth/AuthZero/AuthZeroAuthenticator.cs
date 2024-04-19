using RandomString4Net;


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
            var authKey = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE , 64);
            return authKey;
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
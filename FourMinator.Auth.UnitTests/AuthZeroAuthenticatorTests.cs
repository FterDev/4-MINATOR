using NUnit.Framework;
using Fourminator.Auth;

namespace FourMinator.Auth.UnitTests
{
    [TestFixture]
    public class AuthZeroAuthenticatorTests
    {
        [Test]
        public void GenerateAuthKey_WhenCalled_ShouldReturnAuthKey()
        {
            // Arrange
            var AuthZeroAuthenticator = new AuthZeroAuthenticator();
            var awaitedStringLength = 64;

            // Act
            var result = AuthZeroAuthenticator.GenerateAuthKey();

            // Assert
            Assert.That(result.Length, Is.EqualTo(awaitedStringLength));
          
        }

        [Test]
        public void GenerateAuthKey_WhenCalled_ContainsUpperCase()
        {
            // Arrange
            var AuthZeroAuthenticator = new AuthZeroAuthenticator();

            // Act
            var result = AuthZeroAuthenticator.GenerateAuthKey();

            // Assert
            Assert.That(result.Any(char.IsUpper), Is.True);
          
        }

        [Test]
        public void GenerateAuthKey_WhenCalled_ContainsLowerCase()
        {
            // Arrange
            var AuthZeroAuthenticator = new AuthZeroAuthenticator();

            // Act
            var result = AuthZeroAuthenticator.GenerateAuthKey();

            // Assert
            Assert.That(result.Any(char.IsLower), Is.True);
        }

        [Test]
        public void GenerateAuthKey_WhenCalled_ContainsNumber()
        {
            // Arrange
            var AuthZeroAuthenticator = new AuthZeroAuthenticator();

            // Act
            var result = AuthZeroAuthenticator.GenerateAuthKey();

            // Assert
            Assert.That(result.Any(char.IsDigit), Is.True);
        }

        [Test]
        public void DecodeAuthKey_OnCall_ReturnDecodedAuthKey()
        {
            // Arrange
            var AuthZeroAuthenticator = new AuthZeroAuthenticator();
            var authKeyBase64 = "MlZLcGhibmQ0ZUR1a0xKNVg3Z0g2ankxQlkzMzFUbE9xR3BqU2dRdjBDUE40YnNhb0N3SENBVldPZjRTVmZDdg==";
            var awaitedResult = "2VKphbnd4eDukLJ5X7gH6jy1BY331TlOqGpjSgQv0CPN4bsaoCwHCAVWOf4SVfCv";

            // Act
            var result = AuthZeroAuthenticator.DecodeAuthKey(authKeyBase64);

            // Assert
            Assert.That(result, Is.EqualTo(awaitedResult));
        }


        [Test]
        public void DecodeAuthKey_OnCallWithEmptyString_ReturnsEmptyString()
        {
            // Arrange
            var AuthZeroAuthenticator = new AuthZeroAuthenticator();
            var authKeyBase64 = "";
            var awaitedResult = "";

            // Act
            var result = AuthZeroAuthenticator.DecodeAuthKey(authKeyBase64);

            // Assert
            Assert.That(result, Is.EqualTo(awaitedResult));
        }
    }

    
}
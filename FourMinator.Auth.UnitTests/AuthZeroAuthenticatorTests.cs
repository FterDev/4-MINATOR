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
    }

    
}
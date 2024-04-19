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
            var awaitedResult = "";

            // Act
            var result = AuthZeroAuthenticator.GenerateAuthKey();

            // Assert
            Assert.That(result, Is.EqualTo(awaitedResult));
          
        }
    }

    
}
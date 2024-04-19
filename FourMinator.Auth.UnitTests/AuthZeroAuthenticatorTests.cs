using Xunit;
using Moq;
using FluentAssertions;
using Fourminator.Auth;

namespace FourMinator.Auth.UnitTests
{
    public class AuthZeroAuthenticatorTests
    {
        [Fact]
        public void GenerateAuthKey_WhenCalled_ShouldReturnAuthKey()
        {
            // Arrange
            var AuthZeroAuthenticator = new AuthZeroAuthenticator();
            var awaitedResult = "";

            // Act
            var result = AuthZeroAuthenticator.GenerateAuthKey();

            // Assert
            result.Should().Be(awaitedResult);
          
        }
    }
}
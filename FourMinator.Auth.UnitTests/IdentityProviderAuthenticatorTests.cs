using NUnit.Framework;
using FluentAssertions;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace FourMinator.Auth.UnitTests
{
    [TestFixture]
    public class IdentityProviderAuthenticatorTests
    {
        [Test]
        public void GenerateAuthKey_WhenCalled_ShouldReturnAuthKey()
        {

            // Arrange

            var mockDbContext = new Mock<DbContext>();
            var identityProviderAuth = new IdentityProviderAuthenticator(mockDbContext.Object);
            var awaitedStringLength = 64;

            // Act
            var result = identityProviderAuth.GenerateAuthKey();

            // Assert
            Assert.That(result.Length, Is.EqualTo(awaitedStringLength));
          
        }

        [Test]
        public void GenerateAuthKey_WhenCalled_ContainsUpperCase()
        {
            // Arrange
            var mockDbContext = new Mock<DbContext>();
            var identityProviderAuth = new IdentityProviderAuthenticator(mockDbContext.Object);

            // Act
            var result = identityProviderAuth.GenerateAuthKey();

            // Assert
            Assert.That(result.Any(char.IsUpper), Is.True);
          
        }

        [Test]
        public void GenerateAuthKey_WhenCalled_ContainsLowerCase()
        {
            // Arrange
            var mockDbContext = new Mock<DbContext>();
            var identityProviderAuth = new IdentityProviderAuthenticator(mockDbContext.Object);

            // Act
            var result = identityProviderAuth.GenerateAuthKey();

            // Assert
            Assert.That(result.Any(char.IsLower), Is.True);
        }

        [Test]
        public void GenerateAuthKey_WhenCalled_ContainsNumber()
        {
            // Arrange
            var mockDbContext = new Mock<DbContext>();
            var identityProviderAuth = new IdentityProviderAuthenticator(mockDbContext.Object);

            // Act
            var result = identityProviderAuth.GenerateAuthKey();

            // Assert
            Assert.That(result.Any(char.IsDigit), Is.True);
        }

        [Test]
        public void DecodeAuthKey_OnCall_ReturnDecodedAuthKey()
        {
            // Arrange
            var mockDbContext = new Mock<DbContext>();
            var identityProviderAuth = new IdentityProviderAuthenticator(mockDbContext.Object);
            var authKeyBase64 = "MlZLcGhibmQ0ZUR1a0xKNVg3Z0g2ankxQlkzMzFUbE9xR3BqU2dRdjBDUE40YnNhb0N3SENBVldPZjRTVmZDdg==";
            var awaitedResult = "2VKphbnd4eDukLJ5X7gH6jy1BY331TlOqGpjSgQv0CPN4bsaoCwHCAVWOf4SVfCv";

            // Act
            var result = identityProviderAuth.DecodeAuthKey(authKeyBase64);

            // Assert
            Assert.That(result, Is.EqualTo(awaitedResult));
        }


        [Test]
        public void DecodeAuthKey_OnCallWithEmptyString_ReturnsEmptyString()
        {
            // Arrange
            var mockDbContext = new Mock<DbContext>();
            var identityProviderAuth = new IdentityProviderAuthenticator(mockDbContext.Object);
            var authKeyBase64 = "";
            var awaitedResult = "";

            // Act
            var result = identityProviderAuth.DecodeAuthKey(authKeyBase64);

            // Assert
            Assert.That(result, Is.EqualTo(awaitedResult));
        }


        [Test]
        public void CreateIdentityProvider_WhenCalledEmpty_IdentityProvdierHasDefaultValues()
        {
            // Arrange
            var mockDbContext = new Mock<DbContext>();
            var identityProviderAuth = new IdentityProviderAuthenticator(mockDbContext.Object);



            // Act
            identityProviderAuth.CreateIdentityProvider();

            // Assert
            identityProviderAuth.IdentityProvider.Name.Should().Be("untitled");
            identityProviderAuth.IdentityProvider.Domain.Should().Be("");
            identityProviderAuth.IdentityProvider.SourceIp.Should().Be("0.0.0.0");
        }

        [Test]
        public void CreateIdentityProvider_WhenCalledWithName_IdentityProvdierHasOnlyNameSet()
        {
            // Arrange
            var mockDbContext = new Mock<DbContext>();
            var identityProviderAuth = new IdentityProviderAuthenticator(mockDbContext.Object);



            // Act
            identityProviderAuth.CreateIdentityProvider("Test");

            // Assert
            identityProviderAuth.IdentityProvider.Name.Should().Be("Test");
            identityProviderAuth.IdentityProvider.Domain.Should().Be("");
            identityProviderAuth.IdentityProvider.SourceIp.Should().Be("0.0.0.0");
        }

        [Test]
        public void CreateIdentityProvider_WhenCalledWithNameDomain_IdentityProvdierHasOnlyNameDomainSet()
        {
            // Arrange
            var mockDbContext = new Mock<DbContext>();
            var identityProviderAuth = new IdentityProviderAuthenticator(mockDbContext.Object);



            // Act
            identityProviderAuth.CreateIdentityProvider("Test", "test.com");

            // Assert
            identityProviderAuth.IdentityProvider.Name.Should().Be("Test");
            identityProviderAuth.IdentityProvider.Domain.Should().Be("test.com");
            identityProviderAuth.IdentityProvider.SourceIp.Should().Be("0.0.0.0");
        }

        [Test]
        public void CreateIdentityProvider_WhenCalledWithAll_IdentityProvdierHasAllSet()
        {
            // Arrange
            var mockDbContext = new Mock<DbContext>();
            var identityProviderAuth = new IdentityProviderAuthenticator(mockDbContext.Object);



            // Act
            identityProviderAuth.CreateIdentityProvider("Test", "test.com", "127.0.0.1");

            // Assert
            identityProviderAuth.IdentityProvider.Name.Should().Be("Test");
            identityProviderAuth.IdentityProvider.Domain.Should().Be("test.com");
            identityProviderAuth.IdentityProvider.SourceIp.Should().Be("127.0.0.1");
        }

       

       
    }

    
}
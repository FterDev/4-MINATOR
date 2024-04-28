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
            var identityProviderRepoMock = new Mock<IIdentityProviderRepository>();
            var mockDbContext = new Mock<DbContext>();
            var identityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);
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
            var identityProviderRepoMock = new Mock<IIdentityProviderRepository>();
            var identityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);

            // Act
            var result = identityProviderAuth.GenerateAuthKey();

            // Assert
            Assert.That(result.Any(char.IsUpper), Is.True);
          
        }

        [Test]
        public void GenerateAuthKey_WhenCalled_ContainsLowerCase()
        {
            // Arrange
            var identityProviderRepoMock = new Mock<IIdentityProviderRepository>();
            var identityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);

            // Act
            var result = identityProviderAuth.GenerateAuthKey();

            // Assert
            Assert.That(result.Any(char.IsLower), Is.True);
        }

        [Test]
        public void GenerateAuthKey_WhenCalled_ContainsNumber()
        {
            // Arrange
            var identityProviderRepoMock = new Mock<IIdentityProviderRepository>();
            var identityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);

            // Act
            var result = identityProviderAuth.GenerateAuthKey();

            // Assert
            Assert.That(result.Any(char.IsDigit), Is.True);
        }

        [Test]
        public void DecodeAuthKey_OnCall_ReturnDecodedAuthKey()
        {
            // Arrange
            var identityProviderRepoMock = new Mock<IIdentityProviderRepository>();
            var identityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);
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
            var identityProviderRepoMock = new Mock<IIdentityProviderRepository>();
            var identityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);
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
            var identityProviderRepoMock = new Mock<IIdentityProviderRepository>();
            var identityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);



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
            var identityProviderRepoMock = new Mock<IIdentityProviderRepository>();
            var identityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);



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
            var identityProviderRepoMock = new Mock<IIdentityProviderRepository>();
            var identityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);



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
            var identityProviderRepoMock = new Mock<IIdentityProviderRepository>();
            var identityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);



            // Act
            identityProviderAuth.CreateIdentityProvider("Test", "test.com", "127.0.0.1");

            // Assert
            identityProviderAuth.IdentityProvider.Name.Should().Be("Test");
            identityProviderAuth.IdentityProvider.Domain.Should().Be("test.com");
            identityProviderAuth.IdentityProvider.SourceIp.Should().Be("127.0.0.1");
        }

        [Test]
        public void SaveIdentityProvider_WhenCalled_CallCreateIdentityProviderWithoutException()
        {
            // Arrange
            var identityProviderRepoMock = new Mock<IIdentityProviderRepository>();
            
            var identityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);
            identityProviderAuth.CreateIdentityProvider();
            var identityProvider = identityProviderAuth.IdentityProvider;

            var returnedIdentityProvider = identityProviderRepoMock.Setup(x => x.CreateIdentityProvider(It.IsAny<IdentityProvider>())).ReturnsAsync(identityProvider);

            //Act
            identityProviderAuth.SaveIdentityProvider();
            
            //Assert
            identityProviderRepoMock.Verify(x => x.CreateIdentityProvider(It.IsAny<IdentityProvider>()), Times.Once);
        }

        [Test]
        public void SaveIdentityProvider_WhenCalled_ThrowsExceptionIfIdentityProviderHasOtherId()
        { 
            // Arrange
            var identityProviderRepoMock = new Mock<IIdentityProviderRepository>();

            var identityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);
            identityProviderAuth.CreateIdentityProvider();

            var faultyIdentityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);
            faultyIdentityProviderAuth.CreateIdentityProvider();
            var identityProvider = faultyIdentityProviderAuth.IdentityProvider;

            var returnedIdentityProvider = identityProviderRepoMock.Setup(x => x.CreateIdentityProvider(It.IsAny<IdentityProvider>())).ReturnsAsync(identityProvider);

            //Assert
            Assert.That(() => identityProviderAuth.SaveIdentityProvider(), Throws.Exception);

        }

        [Test]
        public void ValidateKey_WhenCalledWithValidKey_ReturnsTrue()
        {
            // Arrange
            var identityProviderRepoMock = new Mock<IIdentityProviderRepository>();

            var identityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);
            var authKeyBase64 = "MlZLcGhibmQ0ZUR1a0xKNVg3Z0g2ankxQlkzMzFUbE9xR3BqU2dRdjBDUE40YnNhb0N3SENBVldPZjRTVmZDdg==";


            var returnedIdentityProvider = identityProviderRepoMock.Setup(x => x.GetIdentityProviderByKey(It.IsAny<string>())).ReturnsAsync(new IdentityProvider());

            //Act
            var result = identityProviderAuth.ValidateAuthKey(authKeyBase64);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public void ValidateKey_WhenCalledWithInvalidKey_ReturnsFalse()
        {
            // Arrange
            var identityProviderRepoMock = new Mock<IIdentityProviderRepository>();

            var identityProviderAuth = new IdentityProviderAuthenticator(identityProviderRepoMock.Object);
            var authKeyBase64 = "MlZLcGhibmQ0ZUR1a0xKNVg3Z0g2ankxQlkzMzFUbE9xR3BqU2dRdjBDUE40YnNhb0N3SENBVldPZjRTVmZDdg==";

            identityProviderRepoMock.Setup(x => x.GetIdentityProviderByKey(It.IsAny<string>())).ReturnsAsync((IdentityProvider)null);

            //Act
            var result = identityProviderAuth.ValidateAuthKey(authKeyBase64);

            //Assert
            result.Should().BeFalse();
        }
    }

    
}
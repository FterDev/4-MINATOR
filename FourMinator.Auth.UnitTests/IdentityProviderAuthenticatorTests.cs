using FluentAssertions;
using FourMinator.Persistence.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FourMinator.Auth.UnitTests
{
    public class IdentityProviderAuthenticatorTests
    {

       

        [Fact]
        public void DecodeAuthKey_Should_Return_Decoded_AuthKey()
        {
            // Arrange
            var identityProviderRepositoryMock = new Mock<IIdentityProviderRepository>();
            var authenticator = new IdentityProviderAuthenticator(identityProviderRepositoryMock.Object)
            {
                IdentityProvider = new IdentityProvider()
            };
            
            var authKeyBase64 = "SGVsbG8gd29ybGQ="; // "Hello world" in base64
            var expectedAuthKey = "Hello world";

            // Act
            var decodedAuthKey = authenticator.DecodeAuthKey(authKeyBase64);

            // Assert
            decodedAuthKey.Should().Be(expectedAuthKey);
        }

        [Fact]
        public void GenerateAuthKey_Should_Return_New_AuthKey()
        {

            // Arrange
            var identityProviderRepositoryMock = new Mock<IIdentityProviderRepository>();
            var authenticator = new IdentityProviderAuthenticator(identityProviderRepositoryMock.Object)
            {
                IdentityProvider = new IdentityProvider()
            };

            // Act
            var authKey = authenticator.GenerateAuthKey();

            // Assert
            authKey.Should().NotBeNullOrEmpty();
            authKey.Length.Should().Be(64);
            authenticator.IdentityProvider.AuthKey.Should().Be(authKey);
        }

        [Fact]
        public void CreateIdentityProvider_Should_Set_IdentityProvider_Properties()
        {
            // Arrange

            var identityProviderRepositoryMock = new Mock<IIdentityProviderRepository>();
            var authenticator = new IdentityProviderAuthenticator(identityProviderRepositoryMock.Object)
            {
                IdentityProvider = new IdentityProvider()
            };

            var identityProviderName = "Test Identity Provider";
            var domain = "test-domain.com";
            var sourceIp = "127.0.0.1";

            // Act
            authenticator.CreateIdentityProvider(identityProviderName, domain, sourceIp);

            // Assert
            authenticator.IdentityProvider.IdentityProviderId.Should().NotBeEmpty();
            authenticator.IdentityProvider.Name.Should().Be(identityProviderName);
            authenticator.IdentityProvider.Domain.Should().Be(domain);
            authenticator.IdentityProvider.SourceIp.Should().Be(sourceIp);
            authenticator.IdentityProvider.IsActive.Should().BeTrue();
        }

        [Fact]
        public async Task SaveIdentityProvider_Should_Call_CreateIdentityProvider_And_Check_IdentityProviderId()
        {
            // Arrange
            var identityProviderRepositoryMock = new Mock<IIdentityProviderRepository>();
            var authenticator = new IdentityProviderAuthenticator(identityProviderRepositoryMock.Object)
            {
                IdentityProvider = new IdentityProvider()
            };
            var newIdentityProvider = new IdentityProvider
            {
                IdentityProviderId = authenticator.IdentityProvider.IdentityProviderId
            };
            identityProviderRepositoryMock.Setup(x => x.CreateIdentityProvider(It.IsAny<IdentityProvider>()))
                .ReturnsAsync(newIdentityProvider);

            // Act
            authenticator.SaveIdentityProvider();

            // Assert
            identityProviderRepositoryMock.Verify(x => x.CreateIdentityProvider(authenticator.IdentityProvider), Times.Once);
        }

        [Fact]
        public async Task ValidateAuthKey_Should_Return_True_If_IdentityProvider_Exists()
        {
            // Arrange
            var identityProviderRepositoryMock = new Mock<IIdentityProviderRepository>();
            var authenticator = new IdentityProviderAuthenticator(identityProviderRepositoryMock.Object)
            {
                IdentityProvider = new IdentityProvider()
            };
            var authKeyBase64 = "SGVsbG8gd29ybGQ="; // "Hello world" in base64
            var authKey = "Hello world";
            var identityProvider = new IdentityProvider();

            identityProviderRepositoryMock.Setup(x => x.GetIdentityProviderByKey(authKey))
                .ReturnsAsync(identityProvider);

            // Act
            var result = authenticator.ValidateAuthKey(authKeyBase64);

            // Assert
            result.Should().BeTrue();
           
        }

        [Fact]
        public void ValidateAuthKey_Should_Return_False_If_IdentityProvider_Does_Not_Exist()
        {
            // Arrange
            var identityProviderRepositoryMock = new Mock<IIdentityProviderRepository>();
            var authenticator = new IdentityProviderAuthenticator(identityProviderRepositoryMock.Object)
            {
                IdentityProvider = new IdentityProvider()
            };
            var authKeyBase64 = "SGVsbG8gd29ybGQ="; // "Hello world" in base64
            var authKey = "Hello world";

            identityProviderRepositoryMock.Setup(x => x.GetIdentityProviderByKey(authKey))
                .ReturnsAsync((IdentityProvider)null);

            // Act
            var result = authenticator.ValidateAuthKey(authKeyBase64);

            // Assert
            result.Should().BeFalse();
            
        }
    }
}

using FluentAssertions;
using Fourminator.Auth;
using FourMinator.Auth.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.Auth.UnitTests
{
    [TestFixture]
    public class IdentityProviderRepositoryTests
    {
       

        [Test]
        public void CreateIdentityProvider_WhenCalled_ShouldReturnCorrectIdentityProviderId()
        {
            // Arrange
            var dbOptions = new DbContextOptionsBuilder<AuthContext>()
                .UseInMemoryDatabase(databaseName: "fourminator")
                .Options;

            var context = new AuthContext(dbOptions);

            var identityProviderRepo = new IdentityProviderRepository(context);

            var identityProvider = new IdentityProvider
            {
                IdentityProviderId = Guid.NewGuid(),
                Name = "Test",
                AuthKey = "Test",
                ClientSecret = "Test",
                ClientId = "Test",
                Domain = "Test",
                SourceIp = "Test",
                IsActive = true
            };

            // Act
            var result = identityProviderRepo.CreateIdentityProvider(identityProvider).Result;

            // Assert
            result.IdentityProviderId.Should().Be(identityProvider.IdentityProviderId);
            result.Name.Should().Be(identityProvider.Name);
            result.IsActive.Should().Be(identityProvider.IsActive);
        }

        [Test]
        public void CreateIdentityProvider_WhenCalledWithNullVals_ShouldThrow()
        {
            // Arrange
            var dbOptions = new DbContextOptionsBuilder<AuthContext>()
                .UseInMemoryDatabase(databaseName: "fourminator")
                .Options;

            var context = new AuthContext(dbOptions);

            var identityProviderRepo = new IdentityProviderRepository(context);

            var identityProvider = new IdentityProvider
            {
         
            };

            // Act
            var result = identityProviderRepo.CreateIdentityProvider(identityProvider).Result;

            // Assert
            result.IdentityProviderId.Should().Be(identityProvider.IdentityProviderId);
            result.Name.Should().Be(identityProvider.Name);
           
        }
    }
}

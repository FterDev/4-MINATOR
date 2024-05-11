using FourMinator.Persistence.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using MockQueryable.Moq;
using Xunit;
using FluentAssertions;

namespace FourMinator.Auth.UnitTests
{
    public class IdentityProviderRepositoryTests
    {
        [Fact]
        public async Task CreateIdentityProvider_ShouldAddIdentityProviderToContext()
        {
            // Arrange
            var identityProvider = new IdentityProvider();
            var dbContextMock = new Mock<DbContext>();
            var repository = new IdentityProviderRepository(dbContextMock.Object);

            // Act
            await repository.CreateIdentityProvider(identityProvider);

            // Assert
            dbContextMock.Verify(x => x.Add(identityProvider), Times.Once);
        }

        [Fact]
        public async Task CreateIdentityProvider_ShouldSaveChangesAsync()
        {
            // Arrange
            var identityProvider = new IdentityProvider();
            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            var repository = new IdentityProviderRepository(dbContextMock.Object);

            // Act
            await repository.CreateIdentityProvider(identityProvider);

            // Assert
            dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task CreateIdentityProvider_ShouldReturnIdentityProvider()
        {
            // Arrange
            var identityProvider = new IdentityProvider();
            var dbContextMock = new Mock<DbContext>();
            var repository = new IdentityProviderRepository(dbContextMock.Object);

            // Act
            var result = await repository.CreateIdentityProvider(identityProvider);

            // Assert
            Assert.Equal(identityProvider, result);
        }

        [Fact]
        public async Task GetIdentityProviderByKey_ShouldReturnIdentityProvider()
        {
            // Arrange
            var key = "key";
            var identityProvider = new IdentityProvider { AuthKey = key };
            var queryableIdentityProviders = new[] { identityProvider }.AsQueryable().BuildMock();
            var dbContextMock = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<IdentityProvider>>();
            dbSetMock.As<IQueryable<IdentityProvider>>().Setup(x => x.Provider).Returns(queryableIdentityProviders.Provider);
            dbSetMock.As<IQueryable<IdentityProvider>>().Setup(x => x.Expression).Returns(queryableIdentityProviders.Expression);
            dbSetMock.As<IQueryable<IdentityProvider>>().Setup(x => x.ElementType).Returns(queryableIdentityProviders.ElementType);
            dbSetMock.As<IQueryable<IdentityProvider>>().Setup(x => x.GetEnumerator()).Returns(queryableIdentityProviders.GetEnumerator());
            dbContextMock.Setup(x => x.Set<IdentityProvider>()).Returns(dbSetMock.Object);
            var repository = new IdentityProviderRepository(dbContextMock.Object);

            // Act
            var result = await repository.GetIdentityProviderByKey(key);

            // Assert
            Assert.Equal(identityProvider, result);
        }

        [Fact]
        public async Task GetIdentityProviderByKey_ShouldReturnNull()
        {
            // Arrange
            var otherKey = "otherKey";
            var key = "key";
            var identityProvider = new IdentityProvider { AuthKey = key };

            var queryableIdentityProviders = new[] { identityProvider }.AsQueryable().BuildMock();
            var dbContextMock = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<IdentityProvider>>();
            dbSetMock.As<IQueryable<IdentityProvider>>().Setup(x => x.Provider).Returns(queryableIdentityProviders.Provider);
            dbSetMock.As<IQueryable<IdentityProvider>>().Setup(x => x.Expression).Returns(queryableIdentityProviders.Expression);
            dbSetMock.As<IQueryable<IdentityProvider>>().Setup(x => x.ElementType).Returns(queryableIdentityProviders.ElementType);
            dbSetMock.As<IQueryable<IdentityProvider>>().Setup(x => x.GetEnumerator()).Returns(queryableIdentityProviders.GetEnumerator());
            dbContextMock.Setup(x => x.Set<IdentityProvider>()).Returns(dbSetMock.Object);
            var repository = new IdentityProviderRepository(dbContextMock.Object);

            // Act
            var result = await repository.GetIdentityProviderByKey(otherKey);

            // Assert
            result.Should().BeNull();
        }


    }
}

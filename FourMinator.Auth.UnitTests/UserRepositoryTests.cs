using FluentAssertions;
using FourMinator.Persistence;
using FourMinator.Persistence.Domain;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace FourMinator.Auth.UnitTests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task CreateUser_Should_AddUserToContext()
        {
            // Arrange
            var nickname = "JohnDoe";
            var email = "johndoe@example.com";
            var user = new User { Nickname = nickname, Email = email };


            var contextMock = new Mock<FourminatorContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            contextMock.Setup(c => c.Set<User>()).Returns(dbSetMock.Object);

            var userRepository = new UserRepository(contextMock.Object);

            // Act
            await userRepository.CreateUser(nickname, email);

            // Assert
            contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task GetUserByEmail_Should_ReturnUserFromContext()
        {
            // Arrange
            var email = "johndoe@example.com";
            var user = new User { Nickname = "JohnDoe", Email = email };
            var users = new List<User> { user };

            var queryableUsers = users.AsQueryable().BuildMock();
            var contextMock = new Mock<FourminatorContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            dbSetMock.As<IQueryable<User>>().Setup(d => d.Provider).Returns(queryableUsers.Provider);
            dbSetMock.As<IQueryable<User>>().Setup(d => d.Expression).Returns(queryableUsers.Expression);
            dbSetMock.As<IQueryable<User>>().Setup(d => d.ElementType).Returns(queryableUsers.ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(d => d.GetEnumerator()).Returns(queryableUsers.GetEnumerator());
            contextMock.Setup(c => c.Set<User>()).Returns(dbSetMock.Object);

            var userRepository = new UserRepository(contextMock.Object);

            // Act
            var result = await userRepository.GetUserByEmail(email);

            // Assert
            result.Should().Be(user);
        }


        [Fact]
        public async Task GetUserByEmail_Should_ReturnNull()
        {
            // Arrange
            var email = "johndoe@example.com";
            var otherEmail = "asdf@asdf.com";
            var user = new User { Nickname = "JohnDoe", Email = email };
            var users = new List<User> { user };

            var queryableUsers = users.AsQueryable().BuildMock();
            var contextMock = new Mock<FourminatorContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            dbSetMock.As<IQueryable<User>>().Setup(d => d.Provider).Returns(queryableUsers.Provider);
            dbSetMock.As<IQueryable<User>>().Setup(d => d.Expression).Returns(queryableUsers.Expression);
            dbSetMock.As<IQueryable<User>>().Setup(d => d.ElementType).Returns(queryableUsers.ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(d => d.GetEnumerator()).Returns(queryableUsers.GetEnumerator());
            contextMock.Setup(c => c.Set<User>()).Returns(dbSetMock.Object);

            var userRepository = new UserRepository(contextMock.Object);

            // Act
            var result = await userRepository.GetUserByEmail(otherEmail);

            // Assert
            result.Should().Be(null);
        }
    }
}

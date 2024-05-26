using FourMinator.Persistence.Domain;

namespace FourMinator.Auth
{
    public interface IUserRepository
    {
        Task<User?> GetUserByNickname(string nickname);
        Task CreateUser(string nickname, string externalId);
    }
}

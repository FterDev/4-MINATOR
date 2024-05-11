using FourMinator.Persistence.Domain;

namespace FourMinator.Auth
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task CreateUser(string nickname, string email);
    }
}

using FourMinator.Auth;


namespace FourMinator
{
    internal interface IUserService
    {
        Task<User?> GetUserByEmail(string email);
        Task<bool> CreateUser(string nickname, string email);
    }
}

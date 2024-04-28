using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.Auth
{
    internal interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task<bool> CreateUser(string nickname, string email);
    }
}

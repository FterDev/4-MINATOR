
using Microsoft.EntityFrameworkCore;

namespace FourMinator.Auth
{
    internal class UserRepository : IUserRepository
    {
        private readonly DbContext _context;
        public UserRepository(DbContext context) 
        {
            _context = context;    
        }
        public Task<bool> CreateUser(string nickname, string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
    
  
}


using FourMinator.Persistence.Domain;
using Microsoft.EntityFrameworkCore;

namespace FourMinator.Auth
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;
        public UserRepository(DbContext context) 
        {
            _context = context;    
        }
        public async Task CreateUser(string nickname, string email)
        {
            var res = await  _context.AddAsync(new User { Nickname = nickname, Email = email });
            _context.SaveChanges();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
    
}


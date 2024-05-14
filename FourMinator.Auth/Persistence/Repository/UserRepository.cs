
using FourMinator.Persistence;
using FourMinator.Persistence.Domain;
using Microsoft.EntityFrameworkCore;

namespace FourMinator.Auth
{
    public class UserRepository : IUserRepository
    {
        private readonly FourminatorContext _context;
        public UserRepository(FourminatorContext context) 
        {
            _context = context;    
        }
        public async Task CreateUser(string nickname, string email)
        {
            var res = await  _context.Users.AddAsync(new User { Nickname = nickname, Email = email });
            _context.SaveChanges();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
    
}



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
        public async Task CreateUser(string nickname, string externalId)
        {
            var res = await  _context.Users.AddAsync(new User { Nickname = nickname, ExternalId = externalId });
            _context.SaveChanges();
        }

        public async Task<User?> GetUserByNickname (string nickname)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Nickname == nickname);
            return user;
        }
    }
    
}


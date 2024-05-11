using FourMinator.Persistence.Domain;
using Microsoft.EntityFrameworkCore;

namespace FourMinator.Auth
{
    public class IdentityProviderRepository : IIdentityProviderRepository
    {

        private DbContext _context;

        public IdentityProviderRepository(DbContext context) 
        { 
            _context = context;
        }
        public Task<IdentityProvider> CreateIdentityProvider(IdentityProvider identityProvider)
        {
            _context.Add(identityProvider);
            return _context.SaveChangesAsync().ContinueWith(_ => identityProvider);
        }

        public async Task<IdentityProvider?> GetIdentityProviderByKey(string key)
        {
            var identityProvider = await _context.Set<IdentityProvider>().FirstOrDefaultAsync(x => x.AuthKey == key);
            return identityProvider;
        }
    }
}


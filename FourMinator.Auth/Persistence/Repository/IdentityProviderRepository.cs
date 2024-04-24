﻿using Fourminator.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.Auth.Persistence.Repository
{
    internal class IdentityProviderRepository : IIdentityProviderRepository
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
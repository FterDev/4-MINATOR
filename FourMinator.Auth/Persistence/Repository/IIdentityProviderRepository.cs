using Fourminator.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.Auth.Persistence.Repository
{
    public interface IIdentityProviderRepository
    {
        Task<IdentityProvider> CreateIdentityProvider(IdentityProvider identityProvider);
        Task<IdentityProvider> GetIdentityProviderByKey(string key);

    }
}


using FourMinator.Persistence.Domain;

namespace FourMinator.Auth
{
    public interface IIdentityProviderRepository
    {
        Task<IdentityProvider> CreateIdentityProvider(IdentityProvider identityProvider);
        Task<IdentityProvider?> GetIdentityProviderByKey(string key);

    }
}

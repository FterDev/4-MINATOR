using Fourminator.Auth;
using Microsoft.EntityFrameworkCore;

namespace FourMinator.Auth.Persistence
{
    public class AuthContext : DbContext
    {


        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }


        public DbSet<IdentityProvider> IdentityProviders { get; set; }

        // DbSet properties for your entities go here

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityProvider>().HasKey(x => x.IdentityProviderId);
        }
    }
}
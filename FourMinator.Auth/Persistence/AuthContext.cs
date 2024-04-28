using Microsoft.EntityFrameworkCore;

namespace FourMinator.Auth
{
    public class AuthContext : DbContext
    {


        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }

        public AuthContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "server=localhost;port=3306;database=fourminator;user=fmadm;password=4minatorDev24";
                var serverVersion = MySqlServerVersion.AutoDetect(connectionString);
                optionsBuilder.UseMySql(connectionString, serverVersion);
            }
        }


        public DbSet<IdentityProvider> IdentityProviders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityProvider>().HasKey(x => x.IdentityProviderId);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
        }
    }
}
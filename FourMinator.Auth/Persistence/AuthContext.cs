using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace FourMinator.Auth
{
    public class AuthContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.secret.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AuthContext");
            var serverVersion = MySqlServerVersion.AutoDetect(connectionString);
            optionsBuilder.UseMySql(connectionString, serverVersion);
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
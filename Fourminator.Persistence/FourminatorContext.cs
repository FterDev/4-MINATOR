using FourMinator.Persistence.Domain;
using FourMinator.Persistence.Domain.Game;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace FourMinator.Persistence
{
    public class FourminatorContext : DbContext
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
        public DbSet<Robot> Robots { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityProvider>().HasKey(x => x.IdentityProviderId);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasMany(x => x.Robots).WithOne(x => x.CreatedByUser);
            modelBuilder.Entity<Robot>().HasKey(x => x.Id);
            modelBuilder.Entity<Player>().HasKey(x => x.Id);
            modelBuilder.Entity<Player>().HasOne(x => x.User).WithOne(x => x.Player);
        }
    }
}

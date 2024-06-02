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
            optionsBuilder.UseMySql(connectionString, serverVersion, options => options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: System.TimeSpan.FromSeconds(30), errorNumbersToAdd: null));
            
        }


        public DbSet<IdentityProvider> IdentityProviders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Robot> Robots { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchMoves> MatchMoves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityProvider>().HasKey(x => x.IdentityProviderId);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasMany(x => x.Robots).WithOne(x => x.CreatedByUser);
            modelBuilder.Entity<Robot>().HasKey(x => x.Id);
            modelBuilder.Entity<Player>().HasKey(x => x.Id);
            modelBuilder.Entity<Player>().HasOne(x => x.User).WithOne(x => x.Player);
            modelBuilder.Entity<Match>().HasOne(x => x.PlayerYellow).WithMany(x => x.MatchesAsYellow).HasForeignKey(x => x.PlayerYellowId);
            modelBuilder.Entity<Match>().HasOne(x => x.PlayerRed).WithMany(x => x.MatchesAsRed).HasForeignKey(x => x.PlayerRedId);
            modelBuilder.Entity<Match>().HasOne(x => x.Robot).WithMany(x => x.Matches).HasForeignKey(x => x.RobotId);
            modelBuilder.Entity<Match>().HasOne(x => x.PlayerWinner).WithMany(x => x.MatchesAsWinner).HasForeignKey(x => x.WinnerId);
            modelBuilder.Entity<Match>().HasKey(x => x.Id);
            modelBuilder.Entity<MatchMoves>().HasKey(x => new { x.MatchId, x.MoveNumber });
        }
    }
}

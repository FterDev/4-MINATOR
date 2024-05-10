using FourMinator.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;



namespace FourMinator.Robot.Persistence
{
    internal class RobotContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.secret.json")
                .Build();

            var connectionString = configuration.GetConnectionString("RobotContext");
            var serverVersion = MySqlServerVersion.AutoDetect(connectionString);
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }


        public DbSet<Robot> Robots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Robot>().HasKey(x => x.Id);
        }


    }
}

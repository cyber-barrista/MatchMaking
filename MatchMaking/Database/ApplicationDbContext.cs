using MatchMaking.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchMaking.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Game>()
                .Property(e => e.GameName)
                .HasConversion<string>();

            modelBuilder
                .Entity<GameSession>()
                .Property(e => e.State)
                .HasConversion<string>();

            modelBuilder
                .Entity<PlayerRequest>()
                .Property(e => e.RequestType)
                .HasConversion<string>();
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameSession> GameSessions { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerRequest> PlayerRequests { get; set; }
    }
}
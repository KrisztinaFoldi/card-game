using Microsoft.EntityFrameworkCore;

namespace card_game
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Deck> Decks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Card>().ToTable("Cards");
            modelBuilder.Entity<Card>().HasKey(p => p.CardId);
            modelBuilder.Entity<Card>().Property(p => p.CardId).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(p => p.UserId);
            modelBuilder.Entity<User>().Property(p => p.UserId).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(p => p.UserName).IsRequired();
            //modelBuilder.Entity<User>().HasData
            //(
            //    new User {UserId = "firstOpponent", UserName = "first" },
            //    new User {UserId = "secondOpponent", UserName = "second" }
            //);

        }
    }
}
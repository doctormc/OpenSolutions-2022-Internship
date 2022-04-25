using TaskTwo.Models;
namespace TaskTwo.Data
{
    public class APIContext : DbContext
    {
        public DbSet<GameDB> Games { get; set; }
        public DbSet<ReviewDB> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDB>()
                .HasMany(p=>p.ReviewList)
                .WithOne(x=>x.Game)
                .HasForeignKey(y=>y.GameID);
        }
    }
}

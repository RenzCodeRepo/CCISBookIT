using CCISBookIT.Models;
using Microsoft.EntityFrameworkCore;

namespace CCISBookIT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet properties for your entities
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }

        // Optional: Override OnModelCreating if you need to configure entity relationships or other model settings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships, keys, indices, etc.
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomNo);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.FacultyId);


            base.OnModelCreating(modelBuilder);
        }
    }
}

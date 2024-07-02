using Microsoft.EntityFrameworkCore;
using CCISBookIT.Models;
using CCISBookIT.Data.Enum;
using System;

namespace CCISBookIT.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {

        // DbSet properties for your entities
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }

        // Override OnModelCreating to configure entity relationships or other model settings
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

using Microsoft.EntityFrameworkCore;
using CCISBookIT.Models;
using CCISBookIT.Data.Enum;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CCISBookIT.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>   
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        // DbSet properties for your entities
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

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
                .HasForeignKey(b => b.FacultyID)
                .HasPrincipalKey(u => u.FacultyID);

            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.FacultyID)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}

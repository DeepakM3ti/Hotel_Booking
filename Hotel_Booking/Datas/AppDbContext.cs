using Microsoft.EntityFrameworkCore;
using Otelier.BookingService.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Otelier.BookingService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Bookings)
                .WithOne(b => b.Hotel)
                .HasForeignKey(b => b.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

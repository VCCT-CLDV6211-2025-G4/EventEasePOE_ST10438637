using Microsoft.EntityFrameworkCore;
using EventEasePOE.Models;
using EventEasePOE.Controllers;

namespace EventEasePOE.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet <VenueM> Venues { get; set; }
        public DbSet <EventsM> Events { get; set; }
        public DbSet <BookingsM> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<BookingsM>()
                .HasIndex(b => new { b.VenueId, b.BookingDate, b.StartDate, b.EndDate })
                .IsUnique();

            modelBuilder.Entity<BookingsM>()
                .HasOne(b => b.Venue)
                .WithMany(v => v.Bookings)
                .HasForeignKey(b => b.VenueId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookingsM>()
                .HasOne(b => b.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EventId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}


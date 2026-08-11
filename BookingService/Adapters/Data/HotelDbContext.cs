using Data.Guest;
using Data.Room;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        public virtual DbSet<Domain.Guest.Entities.GuestEntity> Guests { get; set; }

        public virtual DbSet<Domain.Room.Entities.RoomEntity> Rooms { get; set; }

        public virtual DbSet<BookingEntity> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GuestConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
        }
    }
}

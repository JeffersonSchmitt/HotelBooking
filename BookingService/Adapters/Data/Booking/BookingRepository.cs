using Domain.Booking.Ports;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Booking
{
    public class BookingRepository : IBookingRepository
    {
        private HotelDbContext _hotelDbContext;
        public BookingRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }
        public async Task<int> CreateBooking(BookingEntity booking)
        {
            _hotelDbContext.Bookings.Add(booking);
            await _hotelDbContext.SaveChangesAsync();
            return booking.Id;
        }

        Task<BookingEntity> IBookingRepository.GetBooking(int id)
        {
            return _hotelDbContext.Bookings.Where(b => b.Id == id).FirstOrDefaultAsync();
        }
    }
}

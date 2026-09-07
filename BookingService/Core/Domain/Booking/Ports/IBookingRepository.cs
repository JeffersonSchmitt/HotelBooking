using Domain.Entities;

namespace Domain.Booking.Ports
{
    public interface IBookingRepository
    {
        Task<BookingEntity> GetBooking(int id);
        Task<int> CreateBooking(BookingEntity booking);
    }
}

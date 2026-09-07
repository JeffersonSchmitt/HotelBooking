using Domain.Booking.Enums;
using Domain.Entities;
using Domain.Guest.Entities;
using Domain.Room.Entities;

namespace Application.Booking.DTO
{
    public class BookingDTO
    {
        public BookingDTO()
        {
            this.PlacedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int RoomId { get; set; }
        public int GuestId { get; set; }
        private Status Status { get; set; }

        public static BookingEntity MapToEntity(BookingDTO bookingDTO)
        {
            return new BookingEntity
            {
                Id = bookingDTO.Id,
                Start = bookingDTO.Start,
                End = bookingDTO.End,
                Room = new RoomEntity { Id = bookingDTO.RoomId },
                Guest = new GuestEntity { Id = bookingDTO.GuestId },
                PlacedAt = bookingDTO.PlacedAt
            };
        }
        public static BookingDTO MapToDto(BookingEntity booking)
        {
            return new BookingDTO
            {
                Id = booking.Id,
                End = booking.End,
                GuestId = booking.Guest.Id,
                PlacedAt = booking.PlacedAt,
                RoomId = booking.Room.Id,
                Start = booking.Start,
            };
        }
    }

}

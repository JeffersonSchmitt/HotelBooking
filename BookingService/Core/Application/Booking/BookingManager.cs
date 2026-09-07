using Application.Booking.DTO;
using Application.Booking.Ports;
using Application.Booking.Requests;
using Application.Booking.Responses;
using Application.Guest.Responses;
using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Guest.Ports;
using Domain.Room.Ports;
using Domain.Shared.Exceptions;

namespace Application.Booking
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;
        public BookingManager(IBookingRepository bookingRepository,
            IGuestRepository guestRepository,
            IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
        }
        public async Task<BookingResponse> CreateBooking(CreateBookingRequest request)
        {
            try
            {
                var booking = BookingDTO.MapToEntity(request.Data);
                booking.Guest = await _guestRepository.Get(request.Data.GuestId);
                booking.Room = await _roomRepository.GetRoom(request.Data.RoomId);

                await booking.Save(_bookingRepository);
                request.Data.Id = booking.Id;

                return new BookingResponse
                {
                    Data = request.Data,
                    Success = true
                };
            }
            catch (MissingRequiredInformationException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = Response.ErrorCodes.MISSING_REQUIRED_INFORMATION,
                    Message = "Missing required information"
                };

            }
            catch (RoomCannotBeBookedException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = Response.ErrorCodes.MISSING_REQUIRED_INFORMATION,
                    Message = "Não pode ser reservado"
                };

            }

        }

        public async Task<BookingResponse> GetBooking(int id)
        {
            var booking = await _bookingRepository.GetBooking(id);

            if (booking == null)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = Response.ErrorCodes.NOT_FOUND,
                    Message = "Guest not found"
                };
            }

            return new BookingResponse
            {
                Data = BookingDTO.MapToDto(booking),
                Success = true,
            };
        }
    }
}

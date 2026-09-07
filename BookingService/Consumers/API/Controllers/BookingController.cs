using Application.Booking.DTO;
using Application.Booking.Ports;
using Application.Booking.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingManager _bookingManager;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IBookingManager bookingManager, ILogger<BookingController> logger)
        {
            _bookingManager = bookingManager;
            _logger = logger;
        }

        [HttpPost(Name = "CreateBooking")]
        public async Task<ActionResult<BookingDTO>> Post(BookingDTO booking)
        {
            var request = new CreateBookingRequest
            {
                Data = booking
            };
            var result = await _bookingManager.CreateBooking(request);

            if (result.Success) return Created("Hospede cadastrado com sucesso", result.Data);

            if (result.ErrorCode == Application.Response.ErrorCodes.MISSING_REQUIRED_INFORMATION)
            {
                return BadRequest(result);
            }
            _logger.LogError("Error creating guest: {ErrorCode}", result.ErrorCode);
            return BadRequest(500);
        }
        [HttpGet("{id}", Name = "GetBooking")]
        public async Task<ActionResult<BookingDTO>> Get(int id)
        {
            var result = await _bookingManager.GetBooking(id);
            if (result.Success) return Ok(result.Data);
            if (result.ErrorCode == Application.Response.ErrorCodes.NOT_FOUND)
            {
                return NotFound(result);
            }
            _logger.LogError("Error getting guest: {ErrorCode}", result.ErrorCode);
            return BadRequest(500);
        }
    }
}

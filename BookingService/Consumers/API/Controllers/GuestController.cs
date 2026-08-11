using Application.Guest.DTO;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IGuestManager _guestManager;

        public GuestController(ILogger<GuestController> logger, IGuestManager guestManager)
        {
            _logger = logger;
            _guestManager = guestManager;
        }

        [HttpPost(Name = "CreateGuest")]
        public async Task<ActionResult<GuestDTO>> Post(GuestDTO guest)
        {
            var request = new CreateGuestRequest
            {
                Data = guest
            };

            var result = await _guestManager.CreateGuest(request);

            if (result.Success) return Created("Hospede cadastrado com sucesso", result.Data);

            if (result.ErrorCode == Application.Response.ErrorCodes.NOT_FOUND)
            {
                return BadRequest(result);
            }
            else if (result.ErrorCode == Application.Response.ErrorCodes.INVALID_DOCUMENT)
            {
                return BadRequest(result);
            }
            else if (result.ErrorCode == Application.Response.ErrorCodes.MISSING_REQUIRED_INFORMATION)
            {
                return BadRequest(result);
            }
            else if (result.ErrorCode == Application.Response.ErrorCodes.INVALID_EMAIL)
            {
                return BadRequest(result);
            }
            else if (result.ErrorCode == Application.Response.ErrorCodes.COULD_NOT_STORE_DATA)
            {
                return BadRequest(result);
            }
            _logger.LogError("Error creating guest: {ErrorCode}", result.ErrorCode);
            return BadRequest(500);
        }
        [HttpGet(Name = "GetGuest")]
        public async Task<ActionResult<GuestDTO>> Get(int id)
        {
            var result = await _guestManager.GetGuest(id);

            if (result.Success) return Ok(result.Data);
         
            return NotFound(result);
        }
    }
}

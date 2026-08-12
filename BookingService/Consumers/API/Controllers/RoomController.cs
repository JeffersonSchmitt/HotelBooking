using Application.Room.DTO;
using Application.Room.Ports;
using Application.Room.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController: ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomManager _roomManager;

        public RoomController(ILogger<RoomController> logger, IRoomManager roomManager)
        {
            _logger = logger;
            _roomManager = roomManager;
        }
        [HttpPost(Name = "CreateRoom")]
        public async Task<ActionResult<RoomDTO>> Post(RoomDTO room)
        {
            var request = new CreateRoomRequest
            {
                Data = room
            };
            var result = await _roomManager.CreateRoom(request);

            if (result.Success) return Created("Quarto cadastrado com sucesso", result.Data);

            if (result.ErrorCode == Application.Response.ErrorCodes.NOT_FOUND)
            {
                return BadRequest(result);
            }

            else if (result.ErrorCode == Application.Response.ErrorCodes.MISSING_REQUIRED_INFORMATION)
            {
                return BadRequest(result);
            }

            else if (result.ErrorCode == Application.Response.ErrorCodes.COULD_NOT_STORE_DATA)
            {
                return BadRequest(result);
            }
            else if (result.ErrorCode == Application.Response.ErrorCodes.INVALID_PRICE)
            {
                return BadRequest(result);
            }

            _logger.LogError("Error creating room: {ErrorCode}", result.ErrorCode);
            return BadRequest(500);
        }
        [HttpGet("{id}", Name = "GetRoom")]
        public async Task<ActionResult<RoomDTO>> Get(int id)
        {
            var result = await _roomManager.GetRoom(id);

            if (result.Success) return Ok(result.Data);

            if (result.ErrorCode == Application.Response.ErrorCodes.NOT_FOUND)
            {
                return NotFound(result);
            }

            return NotFound(result);
        }

        [HttpGet("by-name", Name = "GetRoomByName")]
        public async Task<ActionResult<RoomDTO>> GetByName(string name)
        {
            var result = await _roomManager.GetRoomByName(name);

            if (result.Success) return Ok(result.Data);

            if (result.ErrorCode == Application.Response.ErrorCodes.NOT_FOUND)
            {
                return NotFound(result);
            }

            return NotFound(result);
        }

    }
}

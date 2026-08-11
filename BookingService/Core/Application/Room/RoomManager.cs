using Application.Room.DTO;
using Application.Room.Ports;
using Application.Room.Requests;
using Application.Room.Responses;
using Domain.Room.Ports;
using Domain.Shared.Exceptions;

namespace Application.Room
{
    public class RoomManager : IRoomManager
    {
        private IRoomRepository _repository;

        public RoomManager(IRoomRepository roomRepository)
        {
            _repository = roomRepository;
        }

        public async Task<RoomResponse> CreateRoom(CreateRoomRequest request)
        {
            try
            {
                var room = RoomDTO.MapToEntity(request.Data);

                await room.Save(_repository);

                request.Data.Id = room.Id;

                return new RoomResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (MissingRequiredInformationException)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = Response.ErrorCodes.MISSING_REQUIRED_INFORMATION,
                    Message = "Missing required information"
                };
            }
            catch (InvalidPriceException)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = Response.ErrorCodes.INVALID_PRICE,
                    Message = "Invalid price"
                };
            }
            catch (Exception)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = Response.ErrorCodes.COULD_NOT_STORE_DATA,
                    Message = "Could not store data"
                };
            }
        }

        public async Task<RoomResponse> GetRoom(int id)
        {
            var room = await _repository.GetRoom(id);

            if (room == null)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = Response.ErrorCodes.NOT_FOUND,
                    Message = "Room not found"
                };
            }

            return new RoomResponse
            {
                Data = RoomDTO.MapToDto(room),
                Success = true
            };
        }

        public async Task<RoomResponse> GetRoomByName(string name)
        {
            var room = await _repository.GetRoomByName(name);

            if (room == null)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = Response.ErrorCodes.NOT_FOUND,
                    Message = "Room not found"
                };
            }

            return new RoomResponse
            {
                Data = RoomDTO.MapToDto(room),
                Success = true
            };
        }
    }
}
